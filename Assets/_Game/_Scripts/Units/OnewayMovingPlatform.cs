using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class OnewayMovingPlatform : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform m_wayPointA;
        [SerializeField] private Transform m_wayPointB;
        [SerializeField] private float m_moveSpeed = 10f;
        [SerializeField] private float m_linesSize = .1f;
        [SerializeField] private Color m_lineColor;
        private Vector2 m_smoothDampCurrVel = Vector2.zero;
        private bool m_isMovingRight = false;


        #endregion

        #region Built-In Methods
        private void Update()
        {
            _MovePlatform();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.collider == null) return;
            other.transform.parent = transform;
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider == null) return;
            other.transform.parent = null;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = m_lineColor;
            //Handles.DrawPolyLine(m_wayPointA.position, transform.position);
            UnityEditor.Handles.DrawLine(m_wayPointA.position, transform.position, m_linesSize);
            UnityEditor.Handles.DrawLine(m_wayPointB.position, transform.position, m_linesSize);
#endif
        }
        #endregion

        #region Custom Methods

        private void _MovePlatform()
        {
            if (m_isMovingRight)
            {
                transform.position = Vector2.SmoothDamp(transform.position, m_wayPointB.position, ref m_smoothDampCurrVel, m_moveSpeed);
                //transform.Translate(Vector2.right * m_moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, m_wayPointB.position) < 3f)
                    m_isMovingRight = false;
            }
            else
            {
                transform.position = Vector2.SmoothDamp(transform.position, m_wayPointA.position, ref m_smoothDampCurrVel, m_moveSpeed);
                //transform.Translate(Vector2.left * m_moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, m_wayPointA.position) < 3f)
                    m_isMovingRight = true;
            }
        }
        #endregion


    }
}