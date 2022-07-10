using UnityEngine;

namespace Naren_Dev 
{ 
    public class EnemyBehaviour : MonoBehaviour
    {

        private bool m_isMovingRight;

        [SerializeField] private bool isPatrollingMob = false;
        [SerializeField] private float m_patrollingSpeed = 10f;
        [SerializeField]private SpriteRenderer m_spriteRenderer;
        [SerializeField]private Transform m_leftPoint, m_rightPoint;

        private void Awake()
        {
            CheckDependencies();
        }
        private void Update()
        {
            if (isPatrollingMob) DoPatrolling();
            else
                DoHopping();

        }


        private void CheckDependencies()
        {
            if (m_spriteRenderer == null)
                m_spriteRenderer = GetComponent<SpriteRenderer>();
   
            m_isMovingRight = true;
        }
        private void DoPatrolling()
        {
                if (m_isMovingRight)
                {
                Translate(Vector2.right);
                    m_spriteRenderer.flipX = true;
                    if ( transform.position.x > m_rightPoint.position.x)
                         m_isMovingRight = false;
                }
                else
                {
                Translate(Vector2.left);
                    m_spriteRenderer.flipX =false;
                    if (transform.position.x < m_leftPoint.position.x)
                        m_isMovingRight = true;
                }
        }

        private void DoHopping()
        {
            if (m_isMovingRight)
            {
                Translate(transform.up);
                if (transform.position.y> m_rightPoint.position.y)
                    m_isMovingRight = false;
            }
            else
            {
                Translate(-transform.up);
                if (transform.position.y < m_leftPoint.position.y)
                    m_isMovingRight = true;
            }
        }

        private void Translate(Vector2 direction)
        {
            transform.Translate(direction * m_patrollingSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
               other.gameObject.SetActive(false);
            }
        }



    }
}