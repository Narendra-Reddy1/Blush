using UnityEngine;
using UnityEngine.UI;


namespace Naren_Dev
{
    public class InputManager : MonoBehaviour
    {
        #region Singleton
        public static InputManager instance { get; private set; }

        #endregion Singleton

        [SerializeField] private ControlScheme m_controlScheme = ControlScheme.Keyboard;
        [SerializeField] private VariableJoystick m_playerJoystick;
        [SerializeField] private VariableJoystick m_colorWheelJoystick;
        [SerializeField] private CustomButton m_jumpButton;
        private InputActions m_inputActions;
        [HideInInspector]
        public bool hasControlAcces;
        public Vector2 playerAMoveAxis { get; private set; }
        public Vector2 playerBMoveAxis { get; private set; }
        public Vector2 playerAWheelIndex { get; private set; }
        public Vector2 playerBWheelIndex { get; private set; }

        public bool pA_canJump { get; private set; }
        public bool pB_canJump { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                // Destroy(this.gameObject);
            }
            else
                instance = this;

            m_inputActions = new InputActions();
            hasControlAcces = true;
        }


        private void OnEnable()
        {
            m_inputActions.Enable();

            m_jumpButton.onClick.AddListener(_OnJumpButtonClicked);
            m_jumpButton.OnRelease.AddListener(_OnJumpButtonRelease);
            // m_playerJoystick.OnJoystickDrag += _ReadJoystickInput;
            //m_inputActions.Player_A.Movement.performed += _ => ReadInput();
            //m_inputActions.Player_A.Movement.canceled += _ => ReadInput();

            //m_inputActions.Player_A.ColorWheel.performed += _ => ReadInput();
            //m_inputActions.Player_A.ColorWheel.canceled += _ => ReadInput();

            //m_inputActions.Player_A.Jump.started += _ => ReadInput();
            //m_inputActions.Player_A.Jump.canceled += _ => ReadInput();

            //m_inputActions.Player_B.Movement.performed += _ => ReadInput();
            //m_inputActions.Player_B.Movement.canceled += _ => ReadInput();

            //m_inputActions.Player_B.ColorWheel.performed += _ => ReadInput();
            //m_inputActions.Player_B.ColorWheel.canceled += _ => ReadInput();

            //m_inputActions.Player_B.Jump.started += _ => ReadInput();
            //m_inputActions.Player_B.Jump.canceled += _ => ReadInput();
        }
        private void OnDisable()
        {
            m_jumpButton.onClick.RemoveListener(_OnJumpButtonClicked);
            m_jumpButton.OnRelease.RemoveListener(_OnJumpButtonRelease);
            // m_playerJoystick.OnJoystickDrag -= _ReadJoystickInput;
            //m_inputActions.Player_A.Movement.performed -= _ => ReadInput();
            //m_inputActions.Player_A.Movement.canceled -= _ => ReadInput();

            //m_inputActions.Player_A.ColorWheel.performed -= _ => ReadInput();
            //m_inputActions.Player_A.ColorWheel.canceled -= _ => ReadInput();

            //m_inputActions.Player_A.Jump.started -= _ => ReadInput();
            //m_inputActions.Player_A.Jump.canceled -= _ => ReadInput();

            //m_inputActions.Player_B.Movement.performed -= _ => ReadInput();
            //m_inputActions.Player_B.Movement.canceled -= _ => ReadInput();

            //m_inputActions.Player_B.ColorWheel.performed -= _ => ReadInput();
            //m_inputActions.Player_B.ColorWheel.canceled -= _ => ReadInput();


            //m_inputActions.Player_B.Jump.started -= _ => ReadInput();
            //m_inputActions.Player_B.Jump.canceled -= _ => ReadInput();


            m_inputActions.Disable();
            instance = null;
        }
        private void Update()
        {
            _ReadJoystickInput();
            ReadJumpInput();
            // ReadInput();
        }
        public void _OnJumpButtonClicked()
        {
            pB_canJump = true;
        }
        public void _OnJumpButtonRelease()
        {
            pB_canJump = false;
        }

        private void ReadJumpInput()
        {

            pA_canJump = m_inputActions.Player_A.Jump.triggered;
            pB_canJump = m_inputActions.Player_B.Jump.triggered;
            //  return;
            //pA_canJump = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S);
            //pB_canJump = Input.GetKeyDown(KeyCode.C);
            //if (pB_canJump)
            //{
            //    Debug.Log($"PlayerB Can Jump: {pB_canJump}");
            //}
            //Debug.Log("B Jump" + pB_canJump);
        }
        private void _ReadJoystickInput()
        {
            playerBMoveAxis = new Vector2(m_playerJoystick.Horizontal, 0f);
            playerBWheelIndex = m_colorWheelJoystick.Direction.normalized;
        }
        private void ReadInput()
        {
            switch (m_controlScheme)
            {
                case ControlScheme.Touch:
                    playerAMoveAxis = new Vector2(m_playerJoystick.Horizontal, 0f);
                    playerAWheelIndex = m_colorWheelJoystick.Direction.normalized;
                    break;
                case ControlScheme.JoyStick:
                    break;
                default:
                    //Movement
                    playerAMoveAxis = m_inputActions.Player_A.Movement.ReadValue<Vector2>();
                    playerBMoveAxis = m_inputActions.Player_B.Movement.ReadValue<Vector2>();

                    //Color Wheel
                    playerAWheelIndex = m_inputActions.Player_A.ColorWheel.ReadValue<Vector2>();
                    playerBWheelIndex = m_inputActions.Player_B.ColorWheel.ReadValue<Vector2>();
                    break;
            }
            SovereignUtils.Log($"From ReadInput: InputManager: {playerAMoveAxis.normalized} : wheel : {playerAWheelIndex.normalized}");


            //Jump
            //pA_canJump = m_inputActions.Player_A.Jump.triggered;
            //pB_canJump = m_inputActions.Player_B.Jump.triggered;

        }



    }
}