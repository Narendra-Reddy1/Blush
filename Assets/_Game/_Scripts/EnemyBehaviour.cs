using UnityEngine;

namespace Naren_Dev
{
    public enum EnemyState
    {
        Alive,
        Dead
    }
    public class EnemyBehaviour : MonoBehaviour, IInitializer
    {

        public bool isInversed;
        [SerializeField] private EnemyState m_enemyState = EnemyState.Alive;
        [SerializeField] private bool m_isMovingRight;
        [SerializeField] private bool isPatrollingMob = false;
        [SerializeField] private float m_patrollingSpeed = 10f;
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private SpriteRenderer m_sofSpotRenderer;
        [SerializeField] private Transform m_leftPoint, m_rightPoint;

        private void Awake()
        {
            Init();
        }
        private void Update()
        {
            if (m_enemyState == EnemyState.Dead) return;
            if (isPatrollingMob) DoPatrolling();
            else
                DoHopping();
        }


        public void Init()
        {
            if (m_spriteRenderer == null)
                m_spriteRenderer = GetComponent<SpriteRenderer>();
            if (m_sofSpotRenderer == null && isPatrollingMob)
                m_sofSpotRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        private void DoPatrolling()
        {
            if (m_isMovingRight)
            {
                Translate(Vector2.right);
                m_spriteRenderer.flipX = true;
                if (transform.position.x > m_rightPoint.position.x)
                    m_isMovingRight = false;
            }
            else
            {
                Translate(Vector2.left);
                m_spriteRenderer.flipX = false;
                if (transform.position.x < m_leftPoint.position.x)
                    m_isMovingRight = true;
            }
        }

        private void DoHopping()
        {
            if (m_isMovingRight)
            {
                Translate(transform.up);
                if (transform.position.y > m_rightPoint.position.y)
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

        public void UpdateEnemyState(EnemyState state)
        {
            if (m_enemyState == state) return;
            m_enemyState = state;

            switch (m_enemyState)
            {
                case EnemyState.Alive:
                    AliveState();
                    break;
                case EnemyState.Dead:
                    DeadState();
                    break;
            }
        }
        public EnemyState GetEnemyState()
        {
            return m_enemyState;
        }
        private void DeadState()
        {
            GetComponent<Collider2D>().enabled = false;
            m_spriteRenderer.enabled = false;
            m_sofSpotRenderer.enabled = false;
        }
        private void AliveState()
        {
            m_spriteRenderer.enabled = true;
            m_sofSpotRenderer.enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (m_enemyState == EnemyState.Dead) return;
            if (other.transform.CompareTag("Player"))
            {
                GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_PLAYER_DEAD, PlayerState.Dead);
                //other.gameObject.SetActive(false);
            }
        }



    }
}