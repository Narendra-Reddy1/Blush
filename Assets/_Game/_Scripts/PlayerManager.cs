//#define TESTING //Uncomment this to test
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Naren_Dev
{

    public class PlayerManager : MonoBehaviour, IInitializer
    {

        private float playerWidth;
        private RaycastHit2D hit;
        public SpriteRenderer spriteRenderer;
        public SpriteRenderer eyesRenderer;
        [SerializeField] private Rigidbody2D m_playerRb;
        //[SerializeField] private Transform m_playerParent;

        [Tooltip("Point from where to check player detection.")]
        [SerializeField] private Transform m_playerCheck;

        [Tooltip("Point from where to check ground detection.")]
        [SerializeField] private Transform m_groundCheck;

        [Tooltip("Layer to check with ground i.e to to bring collision check only with desired layer.")]
        [SerializeField] private LayerMask layerMask;

        [Tooltip("Distance to check from the player to ground.")]
        [SerializeField] private float m_checkDistance = 0.1f;

        [Tooltip("Box Size to check isPlayerOnHead or not.")]
        [SerializeField] private Vector2 m_boxSize = new Vector2(0.1f, 0.1f);

        [Tooltip("Ground Check Box Size")]
        [SerializeField] private Vector2 gcBoxSize = new Vector2(0.1f, 0.1f);

        [SerializeField] private ParticleSystem m_playerParticleSystem;
        [SerializeField] private List<Collider2D> m_playerColliders;

        [HideInInspector] public bool isGrounded; //boolean to check whether the player is on ground or not.

        [Tooltip("Boolean to check whether player is on head")]
        public bool isPlayerOnHead;

        //public int playerId;
        #region Unity Methods
        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            //playerHeight = spriteRenderer.bounds.size.x / 3;
            playerWidth = spriteRenderer.bounds.size.y / 2;
        }
        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_RESPAWN, Callback_On_Player_Respawned);
            GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_RESPAWN, Callback_On_Player_Respawned);
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
        }
        private void Update()
        {
            switch (GameManager.s_GameState)
            {
                case GameState.GamePlay:
                    if (GlobalVariables.playerState == PlayerState.Dead) return;
                    _CheckSurroundings();
                    break;
            }
#if TESTING
            if (Input.GetKeyDown(KeyCode.P))
                SetupAndPlayPlayerDeathEffect();
            else if (Input.GetKeyDown(KeyCode.O))
                SetupAndShowPlayerRespawnEffect();
#endif
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_groundCheck.position, gcBoxSize);
            Gizmos.DrawWireCube(m_playerCheck.position, m_boxSize);
        }
        #endregion Unity Methods

        #region Custom Methods

        public void Init()
        {
            if (m_playerParticleSystem == null) TryGetComponent(out m_playerParticleSystem);
            if (spriteRenderer == null) TryGetComponent(out spriteRenderer);
            UpdatePlayerState(PlayerState.Alive);
        }

        private void UpdatePlayerState(PlayerState state)
        {
            if (GlobalVariables.playerState != state)
                GlobalVariables.playerState = state;
            switch (GlobalVariables.playerState)
            {
                case PlayerState.Alive:
                    SetupAndShowPlayerRespawnEffect();
                    break;
                case PlayerState.Dead:
                    SetupAndPlayPlayerDeathEffect();
                    break;
            }
        }
        private void RespawnPlayer()
        {
            GravitySwitcher gravity = GetComponent<GravitySwitcher>();
            if (CheckpointSystem.completedCheckPoints.Count > 0)
            {
                Checkpoint cp = CheckpointSystem.completedCheckPoints.Peek();
                transform.position = cp.checkpointPosition;
                if (cp.hasInverseGravity)
                    gravity.SwitchToInverseGravity();
                else
                    gravity.SwitchToNormalGravity();
            }
            else
            {
                transform.position = GlobalVariables.STARTING_POINT;
                if (GlobalVariables.START_POINT_HAS_INVERSE_GRAVITY)
                    gravity.SwitchToInverseGravity();
                else
                    gravity.SwitchToNormalGravity();
            }
        }

        private async void SetupAndShowPlayerRespawnEffect()
        {
            m_playerParticleSystem.Stop();
            ParticleSystem.VelocityOverLifetimeModule vOLT = m_playerParticleSystem.velocityOverLifetime;
            vOLT.radial = new ParticleSystem.MinMaxCurve(0);
            m_playerParticleSystem.Play();
            spriteRenderer.enabled = true;
            eyesRenderer.enabled = true;
            await Task.Delay(300);
            vOLT.radial = new ParticleSystem.MinMaxCurve(Mathf.Lerp(vOLT.radial.constant, -1.5f, 2.5f));
            foreach (Collider2D collider in m_playerColliders)
                collider.enabled = true;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
        private async void SetupAndPlayPlayerDeathEffect()
        {

            SovereignUtils.Log($"Playing Player Death effect");
            ParticleSystem.MainModule main = m_playerParticleSystem.main;
            ParticleSystem.VelocityOverLifetimeModule vOLT = m_playerParticleSystem.velocityOverLifetime;
            m_playerParticleSystem.Stop();
            GetComponent<Rigidbody2D>().isKinematic = true;
            foreach (Collider2D collider in m_playerColliders)
                collider.enabled = false;
            main.startColor = spriteRenderer.color;
            vOLT.radial = new ParticleSystem.MinMaxCurve(-1);
            m_playerParticleSystem.Play();
            spriteRenderer.enabled = false;
            eyesRenderer.enabled = false;
            await Task.Delay(600);
            vOLT.radial = new ParticleSystem.MinMaxCurve(Mathf.Lerp(vOLT.radial.constant, 1, 5f));

        }
        private void _CheckSurroundings()
        {
            isGrounded = Physics2D.BoxCast(m_groundCheck.position, gcBoxSize, 0f, -transform.up, m_checkDistance, layerMask);
            hit = Physics2D.BoxCast(m_playerCheck.position, m_boxSize, 0f, transform.up, m_checkDistance);
            if (hit)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isPlayerOnHead = true;
                }
            }

            else isPlayerOnHead = false;
        }


        private void _CalculateBounds()
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, PlayerCameraController.MinScreenBound + playerWidth,
                PlayerCameraController.MaxScreenBound - playerWidth);
            transform.position = viewPos;
        }


        #endregion Custom Methods
        #region Callbacks
        public void Callback_On_Player_Dead(object args)
        {
            PlayerState state = (PlayerState)args;
            // m_playerRb.velocity = Vector2.zero;
            UpdatePlayerState(state);
        }
        private void Callback_On_Player_Respawned(object args)
        {
            PlayerState state = (PlayerState)args;
            RespawnPlayer();
            UpdatePlayerState(state);
        }
        #endregion

    }
}