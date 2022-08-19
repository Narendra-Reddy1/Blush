using UnityEngine;

namespace Naren_Dev
{
    /// <summary>
    /// This script responsible to switch the player gravity
    /// </summary>
    public class GravitySwitcher : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRb;
        public bool isGravitySwitched = false;
        private void Awake()
        {
            if (playerRb == null)
                playerRb = GetComponent<Rigidbody2D>();
        }
        public void SwitchToNormalGravity()
        {
            if (playerRb.gravityScale > 0) return;
            playerRb.gravityScale = Mathf.Abs(playerRb.gravityScale);
            isGravitySwitched = false;

        }
        public void SwitchToInverseGravity()
        {
            if (playerRb.gravityScale < 0) return;
            playerRb.gravityScale = -playerRb.gravityScale;
            isGravitySwitched = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "NormalGravity":
                    SwitchToNormalGravity();
                    break;
                case "InverseGravity":

                    SwitchToInverseGravity();
                    break;
            }
        }
    }
}