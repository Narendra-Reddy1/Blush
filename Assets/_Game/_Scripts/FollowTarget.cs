using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    [RequireComponent(typeof(Camera))]
    public class FollowTarget : MonoBehaviour
    {
        public List<Transform> m_targets = new List<Transform>();

        //   private Vector3 m_offSet = new Vector3(0f, 0f,-10);
        private Bounds bounds;
        private Camera m_camera;
        private Vector3 m_middlePos;
        private Vector3 m_currDampVelocity;
        private float vel = 0f;
        [Tooltip("Higher value smoothens. Lower value fastens")]
        [SerializeField] private float m_transitionVelocity = 25f;
        [SerializeField] private float m_smoothDampVelociy = .25f;
        [SerializeField] private float m_minCamSize = 8, m_maxCamSize = 15;
        private void Awake()
        {
            m_camera = Camera.main;
        }
        private void LateUpdate()
        {
            if (m_targets.Count == 0)
                return;
            TrackTheTarget();
            AdjustCameraView();
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
            m_middlePos.y += 3f;
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
        private void OnDrawGizmos()
        {

            Gizmos.DrawIcon(GetCenter(), "center");
        }

        private void OnDisable()
        {
            m_targets = null;
        }

    }




}