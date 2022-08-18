using UnityEngine;

namespace Naren_Dev
{

    public struct Checkpoint
    {
        public Vector2 postion;
    }

    public class CheckpointSystem : MonoBehaviour
    {

        #region Unity Methods

        public void OnEnable()
        {
            EventHandler.AddListener(EventID.EVENT_ON_CHECKPOINT_REACHED, Callback_On_Checkpoint_Reached);
        }
        private void OnDisable()
        {
            EventHandler.RemoveListener(EventID.EVENT_ON_CHECKPOINT_REACHED, Callback_On_Checkpoint_Reached);
        }
        #endregion Unity Methods

        #region Callbacks

        public void Callback_On_Checkpoint_Reached(object args)
        {

        }

        #endregion Callbacks
    }
}