using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace Naren_Dev
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance { get; private set; }

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
                Destroy(this.gameObject);
            }
            else
                instance = this;

            m_inputActions = new InputActions();
            hasControlAcces = true;

        }

        private void OnEnable()
        {
            m_inputActions.Enable();
            m_inputActions.Player_A.Movement.performed += _ => ReadInput();
            m_inputActions.Player_A.Movement.canceled += _ => ReadInput();

            m_inputActions.Player_A.ColorWheel.performed += _ => ReadInput();
            m_inputActions.Player_A.ColorWheel.canceled += _ => ReadInput();

            m_inputActions.Player_A.Jump.started += _ => ReadInput();
            m_inputActions.Player_A.Jump.canceled += _ => ReadInput();

            m_inputActions.Player_B.Movement.performed += _ => ReadInput();
            m_inputActions.Player_B.Movement.canceled += _ => ReadInput();

            m_inputActions.Player_B.ColorWheel.performed += _ => ReadInput();
            m_inputActions.Player_B.ColorWheel.canceled += _ => ReadInput();

            m_inputActions.Player_B.Jump.started += _ => ReadInput();
            m_inputActions.Player_B.Jump.canceled += _ => ReadInput();
        }
        private void OnDisable()
        {
            m_inputActions.Player_A.Movement.performed -= _ => ReadInput();
            m_inputActions.Player_A.Movement.canceled -= _ => ReadInput();

            m_inputActions.Player_A.ColorWheel.performed -= _ => ReadInput();
            m_inputActions.Player_A.ColorWheel.canceled -= _ => ReadInput();

            m_inputActions.Player_A.Jump.started -= _ => ReadInput();
            m_inputActions.Player_A.Jump.canceled -= _ => ReadInput();

            m_inputActions.Player_B.Movement.performed -= _ => ReadInput();
            m_inputActions.Player_B.Movement.canceled -= _ => ReadInput();

            m_inputActions.Player_B.ColorWheel.performed -= _ => ReadInput();
            m_inputActions.Player_B.ColorWheel.canceled -= _ => ReadInput();


            m_inputActions.Player_B.Jump.started -= _ => ReadInput();
            m_inputActions.Player_B.Jump.canceled -= _ => ReadInput();


            m_inputActions.Disable();
            instance = null;
        }
        private void Update()
        {
            ReadJumpInput();
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

        private void ReadInput()
        {
            //Movement
            playerAMoveAxis = m_inputActions.Player_A.Movement.ReadValue<Vector2>();
            playerBMoveAxis = m_inputActions.Player_B.Movement.ReadValue<Vector2>();

            //Color Wheel
            playerAWheelIndex = m_inputActions.Player_A.ColorWheel.ReadValue<Vector2>();
            playerBWheelIndex = m_inputActions.Player_B.ColorWheel.ReadValue<Vector2>();

            //Jump
            //pA_canJump = m_inputActions.Player_A.Jump.triggered;
            //pB_canJump = m_inputActions.Player_B.Jump.triggered;

        }



    }
}