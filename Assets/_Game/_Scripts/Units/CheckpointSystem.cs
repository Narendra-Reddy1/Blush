using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class CheckpointSystem : MonoBehaviour
    {
        [SerializeField] private Sprite m_checpointEnabledSprite;
        [SerializeField] private Sprite m_checpointDisabledSprite;
        public static Stack<Checkpoint> completedCheckPoints { get; private set; }

        #region Unity Methods
        private void Awake()
        {
            completedCheckPoints = new Stack<Checkpoint>();
        }

        public void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_CHECKPOINT_REACHED, Callback_On_Checkpoint_Reached);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_CHECKPOINT_REACHED, Callback_On_Checkpoint_Reached);
        }
        #endregion Unity Methods

        public void OnNewCheckpointReached()
        {
            completedCheckPoints.Peek().GetComponent<SpriteRenderer>().sprite = m_checpointEnabledSprite;
        }

        #region Callbacks

        public void Callback_On_Checkpoint_Reached(object args)
        {
            foreach (Checkpoint cp in completedCheckPoints)
            {
                SovereignUtils.Log($"CP: {cp.name}");
            }
            GameObject checkPoint = (GameObject)args;
          //  if (!completedCheckPoints.Contains(checkPoint.GetComponent<Checkpoint>()))
                completedCheckPoints.Push(checkPoint.GetComponent<Checkpoint>());
            OnNewCheckpointReached();
        }

        #endregion Callbacks
    }
}
