using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Naren_Dev
{
    public class JumpPadBehaviour : MonoBehaviour
    {
        #region Variables
       [SerializeField] private List<Collider2D> m_colliders;
        //private Collider2D[] m_colliders = new Collider2D[2];
        [SerializeField] private BoxCollider2D m_jumpPadCollider;

        #endregion

        #region Custom Methods

        private void _CheckDependencies()
        {
            if (m_jumpPadCollider == null) TryGetComponent(out m_jumpPadCollider);
        }

        #endregion

        #region Built-In Methods

        private void Awake()
        {
            m_colliders = new List<Collider2D>();

            _CheckDependencies();
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            

            switch (other.transform.tag)
            {
                case "Player":
                    //m_colliders.Clear();
                    int i = m_jumpPadCollider.GetContacts(m_colliders);
                    if (i <= 1)
                        return;
                    //if (!l_colliders[0].CompareTag("Player")) return;
                    for (int j = 0; j < i-1; j++)
                    {
                        m_colliders[j].attachedRigidbody.velocity = new Vector2(0f, other.relativeVelocity.y);
                    }
                   // m_colliders[0].attachedRigidbody.velocity = new Vector2(0f, other.relativeVelocity.y);
                    break;
                //case"PushableBox":
                //    if (m_colliders.Count < 2)return;
                //    int count = m_colliders.Count - 1;
                //    for (int j = 0; j < count; j++)
                //    {
                //        m_colliders[0].attachedRigidbody.velocity = new Vector2(0f, other.relativeVelocity.y);
                //    }
                //    break;
            }
        }

        #endregion
    }
}