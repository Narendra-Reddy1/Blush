using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Naren_Dev
{
    public class PlayerCollisionsManager : MonoBehaviour
    {

        #region Unity Built-In Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Collectable":
                    Transform[] args = { transform, other.transform };
                    GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_COLLECTABLE_COLLECTED, args);
                    other.enabled = false;
                    break;
                case "JumpPad":

                    break;
                case "Checkpoint":
                    GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_CHECKPOINT_REACHED, other.gameObject);
                    other.enabled = false;
                    break;
            }
        }

        #endregion
    }
}