using UnityEngine;

namespace Naren_Dev
{

    public class PlayerManager : MonoBehaviour
    {

        private float playerWidth;
        private RaycastHit2D hit;
        public SpriteRenderer spriteRenderer;
        [SerializeField]private Transform m_playerParent;

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

        [HideInInspector] public bool isGrounded; //boolean to check whether the player is on ground or not.
        
        [Tooltip("Boolean to check whether player is on head")]
        public bool isPlayerOnHead; 
        
        //public int playerId;

        private void Awake()
        {
            CheckDependencies();
        }

        private void Start()
        {
            //playerHeight = spriteRenderer.bounds.size.x / 3;
            playerWidth = spriteRenderer.bounds.size.y / 2;
        }
        private void Update()
        {
         
            CheckSurroundings();
            
        }

        private void LateUpdate()
        {
            CalculateBounds();
        }

        private void CheckDependencies()
        {
            if (m_playerParent == null) m_playerParent = transform.parent;

            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }


        private void CheckSurroundings()
        {
            //isGrounded = Physics2D.CircleCast(m_groundCheck.position, gcRadius, -transform.up, m_checkDistance, layerMask);
            // hit = Physics2D.CircleCast(m_playerCheck.position, gcRadius, transform.up, m_groundCheckDistance);

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

      
        void CalculateBounds()
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, GameManager.instance.MinScreenBound +playerWidth,
                GameManager.instance.MaxScreenBound - playerWidth);
            transform.position = viewPos;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_groundCheck.position, gcBoxSize);
            //  Gizmos.DrawWireSphere(m_playerCheck.position, gcRadius);

            Gizmos.DrawWireCube(m_playerCheck.position, m_boxSize);
        }

        


    }
}