using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naren_Dev
{
    public class PushableBox : MonoBehaviour
    {

        #region Variables

        [Range(1, 4)]
        [SerializeField] private int m_pushCount = 1;
        [SerializeField] private Collider2D m_groundCollider;
        //[SerializeField] private LayerMask m_layermask;
        [SerializeField] private Rigidbody2D m_pushableRb;
        [SerializeField] private BoxCollider2D m_pushBoxCollider;
        private Collider2D[] m_colliders = new Collider2D[2];
        List<Collider2D> contactColliders;
        //public float speed = 1f;

        #endregion

        #region Built-In Methods

        private void Awake()
        {
            _Init();

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.collider.tag)
            {
                case "Player":
                    contactColliders=new List<Collider2D>();
                    int i = m_pushBoxCollider.GetContacts(contactColliders);
                    //contactColliders = m_colliders.ToList();
                    if (contactColliders.Contains(m_groundCollider))
                        contactColliders.Remove(m_groundCollider);
                    Debug.Log($"++ Count: {i}");
                    if (contactColliders.Count == m_pushCount)
                    {
                        m_pushableRb.isKinematic = false;
                        m_pushableRb.constraints = RigidbodyConstraints2D.None;
                        m_pushableRb.freezeRotation = true;
                        Debug.Log("Enter constraints: " + m_pushableRb.constraints);
                    }
                    break;
                default:
                    break;
            }
        }


        /*    private void OnCollisionStay2D(Collision2D other)
            {
                switch (other.collider.tag)
                {
                    case "Player":
                        Vector3 offSet = transform.position - other.transform.position;
                        offSet.y = 0;
                        Vector3 newPos = new Vector3(other.transform.position.x, 0) + offSet;
                        Vector2 vel = Vector2.zero;
                        Debug.Log(*//*new Vector2(other.transform.position.x, 0)+*//* offSet);
                        transform.position = Vector2.SmoothDamp(transform.position, transform.position + offSet, ref vel, speed);
                        break;
                }
            }*/

        private void OnCollisionExit2D(Collision2D other)
        {
            switch (other.collider.tag)
            {
                case "Player":
                    int i = GetComponent<BoxCollider2D>().GetContacts(m_colliders);
                    if (i <= 1)
                    {
                    }
                    m_pushableRb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    m_pushableRb.isKinematic = true;
                    //m_pushableRb.constraints = RigidbodyConstraints2D.FreezeAll;
                    Debug.Log("Exit constraints: " + m_pushableRb.constraints);
                    m_pushableRb.velocity = Vector2.zero;
                    break;
            }
        }

        #endregion

        #region Custom Methods

        private void _Init()
        {
            if (m_pushableRb == null) TryGetComponent(out m_pushableRb);
            if (m_pushBoxCollider == null) TryGetComponent(out m_pushBoxCollider);
        }

        #endregion

    }
}