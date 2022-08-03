using UnityEngine;

namespace Naren_Dev
{

    public class GravitySwitcher : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRb;
        /*public bool isVelocityGained = false;
        public float maxVelocity;*/
        public bool isGravitySwitched = false;
        private void Awake()
        {
            if (playerRb == null)
                playerRb = GetComponent<Rigidbody2D>();

        }

        /* private void OnTriggerEnter2D(Collider2D other)
         {
             if (other.CompareTag("NormalGravity"))
             {
                 if (playerRb.gravityScale < 0)
                     playerRb.gravityScale = Mathf.Abs(playerRb.gravityScale);
             }
         }
 */
        private void OnTriggerExit2D(Collider2D other)
        {

            /*if (!isVelocityGained)
            {
                maxVelocity = playerRb.velocity.y;
                isVelocityGained = true;
            }*/

            switch (other.tag)
            {
                case "NormalGravity":
                    if (playerRb.gravityScale < 0)
                    {
                        playerRb.gravityScale = Mathf.Abs(playerRb.gravityScale);
                        isGravitySwitched = false;

                    }
                    break;
                case "InverseGravity":
                    if (playerRb.gravityScale > 0)
                    {

                        playerRb.gravityScale = -playerRb.gravityScale;
                        isGravitySwitched = true;

                    }
                    break;/*
                case "NeutralPortal":
                    AudioManager.instance.PlaySFX(AudioId.)
                    break;*/
            }

/*
            if (other.CompareTag("NormalGravity"))
            {
                if (playerRb.gravityScale < 0)
                {
                    playerRb.gravityScale = Mathf.Abs(playerRb.gravityScale);
                    isGravitySwitched = false;

                }

            }

            if (other.CompareTag("InverseGravity"))
            {
                if (playerRb.gravityScale > 0)
                {

                    playerRb.gravityScale = -playerRb.gravityScale;
                    isGravitySwitched = true;

                }
            }*/
        }
    }
}