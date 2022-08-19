using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class Checkpoint : MonoBehaviour, IInitializer
    {
        #region Variables
        [SerializeField] private Transform m_checkpointPosTransform;
        public bool hasInverseGravity=false;
        public Vector2 checkpointPosition;
        private bool isReached = false;
        #endregion Variables


        #region Unity Methods
        private void OnEnable()
        {
            Init(); //to reduce stack size for unity methods, calling Init in  OnEnable()
            GlobalEventHandler.AddListener(EventID.EVENT_ON_CHECKPOINT_REACHED, Callback_On_Checkpoint_Reached);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_CHECKPOINT_REACHED, Callback_On_Checkpoint_Reached);
        }
        #endregion Unity Methods


        #region  CustomMethods
        public void Init()
        {
            isReached = false;
            checkpointPosition = m_checkpointPosTransform.position;
        }

        #endregion


        #region Callbacks
        private void Callback_On_Checkpoint_Reached(object args)
        {
            isReached = true;
        }


        #endregion Callbacks
    }
}