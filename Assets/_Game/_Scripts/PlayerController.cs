using UnityEngine;

namespace Naren_Dev
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {


        #region Variables


        private GravitySwitcher m_gravitySwitcher;
        [Space]
        [SerializeField] private bool isPlayerA = false;
        [SerializeField] private bool isPlayerB = false;
        [Space]
        [Space]

        [SerializeField] private Vector2 m_eyesOffset;
        [SerializeField] private Animator m_playerAnim;
        [SerializeField] private Animator m_eyesAnim;
        // [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private SpriteRenderer m_eyesRenderer;
        [SerializeField] private GameObject m_eyes;
        [SerializeField] private PlayerManager m_playerManager;
        [SerializeField] private Rigidbody2D m_playerRb;
        [SerializeField] private float m_moveSpeed = 25f;
        [SerializeField] private float m_jumpForce = 25f;
        [SerializeField] private float m_maxVelocity = 20f;

        [Header("Colors")]
        [Space]
        [SerializeField] private Color m_portalColor_1;
        [SerializeField] private Color m_portalColor_2;
        [SerializeField] private Color m_portalColor_3;
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel;
        private Vector2 m_inputAxis;
        private bool canJump;

        /* =========ColorWheelProperties======= */
        private Vector2 m_wheelIndexInput;
        private Color m_defaultColor;
        public enum m_ColorState { Ideal = 0, Color_1 = 1, Color_2 = 2, Color_3 = 3 };
        private m_ColorState colorState;

        #endregion

        #region Properties

        public m_ColorState ColorState
        {
            get
            {
                return colorState;
            }

        }

        #endregion

        #region Built-In Methods

        private void Awake()
        {
            CheckDependecies();

            m_defaultColor = m_playerManager.spriteRenderer.color;

        }

        private void Update()
        {

            if (InputManager.instance.hasControlAcces)
            {
                GetInput();

                GetAndSetColorState();
                ApplyJump();

            }
            else
                HaultPlayer();
            VelocityController();
            SetAnimationIds();


        }


        private void FixedUpdate()
        {
            ApplyMovement();
            FlipPlayer();
        }

        #endregion

        #region Custom Methods

        private void CheckDependecies()
        {

            m_gravitySwitcher = GetComponent<GravitySwitcher>();



            if (m_playerRb == null)
            {
                m_playerRb = GetComponent<Rigidbody2D>();
            }
            if (m_playerManager == null)
            {
                m_playerManager = GetComponent<PlayerManager>();

            }
            //if (m_spriteRenderer == null)
            //{
            //    m_spriteRenderer = GetComponent<SpriteRenderer>();
            //}

            if (m_eyes == null)
            {
                if (isPlayerA)
                {

                    m_eyes = GameObject.Find("Eyes_Player_A");

                }
                else if (isPlayerB)
                {
                    m_eyes = GameObject.Find("Eyes_Player_B");
                }
            }
            if (m_eyesRenderer == null)
            {
                m_eyesRenderer = m_eyes.GetComponent<SpriteRenderer>();
            }
            if (m_playerAnim == null || m_eyesAnim == null)
            {
                m_playerAnim = GetComponent<Animator>();
                m_eyesAnim = m_eyes.GetComponent<Animator>();
            }

        }

        private void GetInput()
        {
            m_inputAxis = isPlayerA ? InputManager.instance.playerAMoveAxis : InputManager.instance.playerBMoveAxis;
            m_wheelIndexInput = isPlayerA ? InputManager.instance.playerAWheelIndex : InputManager.instance.playerBWheelIndex;
            canJump = isPlayerA ? InputManager.instance.pA_canJump : InputManager.instance.pB_canJump;
        }

        #region Player Movement 


        private void ApplyMovement()
        {
            if (!m_gravitySwitcher.isGravitySwitched)
            {

                transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            }
            else
            {

                transform.rotation = Quaternion.Euler(180f, 0f, 0f);
            }

            m_playerRb.velocity = new Vector2(m_inputAxis.x * m_moveSpeed, m_playerRb.velocity.y);

        }

        private void ApplyJump()
        {

            if (canJump && m_playerManager.isGrounded)
            {
                m_playerRb.velocity = Vector2.zero;
                m_audioEventChannel.RaiseSFXPlayEvent(AudioId.JumpSFX);
                // AudioManager.instance?.PlaySFX(AudioId.JumpSFX);
                m_playerRb.velocity = new Vector2(m_playerRb.velocity.x, m_gravitySwitcher.isGravitySwitched ?
                    -m_jumpForce : m_jumpForce *
                    (m_playerManager.isPlayerOnHead ? 5 : 1));

            }

        }

        private void FlipPlayer()
        {
            if (m_inputAxis.x > 0)
            {
                m_playerManager.spriteRenderer.flipX = false;
                m_eyesRenderer.flipX = false;
                m_eyes.transform.localPosition = m_eyesOffset;

            }
            else if (m_inputAxis.x < 0)
            {
                m_playerManager.spriteRenderer.flipX = true;
                m_eyesRenderer.flipX = true;

                Vector2 offset = m_eyesOffset;
                offset.x *= -1;
                m_eyes.transform.localPosition = offset;

            }
        }


        private void VelocityController()
        {
            Vector2 tempVelocity = m_playerRb.velocity;
            tempVelocity.y = Mathf.Clamp(tempVelocity.y, m_playerRb.velocity.y, m_maxVelocity);
            m_playerRb.velocity = tempVelocity;
        }

        private void HaultPlayer()
        {
            m_playerRb.velocity = Vector2.zero;
            m_inputAxis = Vector2.zero;
        }


        #endregion

        private void SetAnimationIds()
        {

            m_playerAnim.SetFloat("walking", m_inputAxis.x);
            m_eyesAnim.SetFloat("walking", m_inputAxis.x);
        }

        #region Color Selection

        private void GetAndSetColorState()
        {
            if (m_wheelIndexInput.y > 0.5f)
            {
                m_playerManager.spriteRenderer.color = m_defaultColor;
                if (isPlayerA)
                {
                    SetIgnoreCollisionLayers();
                }
                else if (isPlayerB)
                {
                    SetIgnoreCollisionLayers();
                }

            }

            else if (m_wheelIndexInput.y < -0.5f)
            {
                m_playerManager.spriteRenderer.color = m_portalColor_2;

                if (isPlayerA)
                {
                    SetIgnoreCollisionLayers(ignoreColor2: true);
                }
                else if (isPlayerB)
                {
                    SetIgnoreCollisionLayers(ignoreColor2: true);
                }
            }

            else if (m_wheelIndexInput.x > 0.5f)
            {
                m_playerManager.spriteRenderer.color = m_portalColor_1;

                if (isPlayerA)
                {
                    SetIgnoreCollisionLayers(ignoreColor1: true);
                }

                else if (isPlayerB)
                {
                    SetIgnoreCollisionLayers(ignoreColor1: true);

                    //  Physics2D.IgnoreLayerCollision(8,14, true); //Color_1
                    //   Physics2D.IgnoreLayerCollision(8,15, false); //Color_2
                    // Physics2D.IgnoreLayerCollision(8,16, false); //Color_3
                }


            }

            else if (m_wheelIndexInput.x < -0.5f)
            {
                m_playerManager.spriteRenderer.color = m_portalColor_3;
                // print(isPlayerB);
                if (isPlayerA)
                {
                    SetIgnoreCollisionLayers(ignoreColor3: true);
                }
                else if (isPlayerB)
                {
                    SetIgnoreCollisionLayers(ignoreColor3: true);
                }

            }
        }


        private void SetIgnoreCollisionLayers(bool ignoreColor1 = false, bool ignoreColor2 = false, bool ignoreColor3 = false)
        {
            if (isPlayerA)
            {
                Physics2D.IgnoreLayerCollision(7, 11, ignoreColor1); //Color_1
                Physics2D.IgnoreLayerCollision(7, 12, ignoreColor2); //Color_2
                Physics2D.IgnoreLayerCollision(7, 13, ignoreColor3); //Color_3
            }
            else if (isPlayerB)
            {
                Physics2D.IgnoreLayerCollision(8, 14, ignoreColor1); //Color_1
                Physics2D.IgnoreLayerCollision(8, 15, ignoreColor2); //Color_2
                Physics2D.IgnoreLayerCollision(8, 16, ignoreColor3); //Color_3
            }

        }

        #endregion


        #endregion


    }
}