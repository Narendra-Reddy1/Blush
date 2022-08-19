using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCameraController : MonoBehaviour, IInitializer
    {
        #region Variables

        //   private Vector3 m_offSet = new Vector3(0f, 0f,-10);
        [SerializeField] private List<Transform> m_targets = new List<Transform>();
        [SerializeField] private Camera m_camera;
        [SerializeField] private Vector3 m_shakeThreshold;
        [SerializeField] private float m_shakeDuration;
        private Bounds bounds;
        private Vector3 m_middlePos;
        private Vector3 m_currDampVelocity;
        private float vel = 0f;

        public static float MinScreenBound => minScreenBounds.x;
        public static float MaxScreenBound => maxScreenBounds.x;


        private static Vector3 minScreenBounds;
        private static Vector3 maxScreenBounds;

        [Tooltip("Higher value smoothens. Lower value fastens")]
        [SerializeField] private float m_transitionVelocity = 25f;
        [SerializeField] private float m_smoothDampVelociy = .25f;
        [SerializeField] private float m_minCamSize = 8, m_maxCamSize = 15;

        #endregion Variables

        #region Unity Methods
        private void Awake()
        {
            //  if (m_camera = null) TryGetComponent(out m_camera);
            Init();
        }
        private void OnDrawGizmos()
        {

            Gizmos.DrawIcon(GetCenter(), "center");
        }
        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
            GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_RESPAWN, Callback_On_Player_Respawned);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_RESPAWN, Callback_On_Player_Respawned);
            m_targets = null;
        }
        private void Update()
        {
            SetScreenBounds();
        }
        private void LateUpdate()
        {
            if (m_targets.Count <= 0)
                return;
            TrackTheTarget();
            //  AdjustCameraView();
        }

        #endregion Unity Methods

        #region Custom Methods
        public void Init()
        {
            if (m_camera == null) m_camera = Camera.main;
        }
        private void ShakeCameraPosition()
        {
            transform.DOPunchPosition(m_shakeThreshold, m_shakeDuration);
        }
        private void SetScreenBounds()
        {
            minScreenBounds = m_camera.ScreenToWorldPoint(new Vector3(0, Screen.height, m_camera.transform.position.z));
            maxScreenBounds = m_camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, m_camera.transform.position.z));
        }

        private void AdjustCameraView()
        {
            Vector2 target1 = m_targets[0].position;
            Vector2 target2 = m_targets[1].position;

            m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize, m_minCamSize, m_maxCamSize);
            if (Vector2.Distance(target1, target2) < 20f)
                m_camera.orthographicSize = Mathf.SmoothDamp(m_camera.orthographicSize, m_minCamSize, ref vel, m_transitionVelocity);
            else if (Vector2.Distance(target1, target2) > 22f)
            {
                m_camera.orthographicSize = Mathf.SmoothDamp(m_camera.orthographicSize, m_maxCamSize, ref vel, m_transitionVelocity);
            }
            //  Debug.Log(Vector2.Distance(target1, target2));

        }

        private void TrackTheTarget()
        {
            m_middlePos = GetCenter();
            m_middlePos.z = -10;
            // m_middlePos.y += 3f;
            // this.transform.position=new Vector3(m_middlePos.x,)
            this.transform.position = Vector3.SmoothDamp(transform.position, m_middlePos, ref m_currDampVelocity, m_smoothDampVelociy);
        }
        private Vector3 GetCenter()
        {

            bounds = new Bounds(m_targets[0].position, Vector3.zero);
            foreach (Transform target in m_targets)
            {
                bounds.Encapsulate(target.position);
            }
            return bounds.center;

        }

        #endregion Custom methods

        #region Callbacks
        public void Callback_On_Player_Dead(object args)
        {
            ShakeCameraPosition();
        }
        public void Callback_On_Player_Respawned(object args)
        {
            ShakeCameraPosition();
        }

        #endregion Callbacks
    }
}