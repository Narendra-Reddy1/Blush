using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Naren_Dev
{
    public class CollectableManager : MonoBehaviour
    {
        #region UnityEvents

        // public UnityEvent<Collectable, Transform> onCollectableCollected = null;

        #endregion

        #region Variables
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel;
        //[SerializeField] private GameObject m_collectableParticlePrefab;

        #endregion

        #region Unity Built-In Methods

        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_COLLECTABLE_COLLECTED, On_CollectableCollected);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_COLLECTABLE_COLLECTED, On_CollectableCollected);
        }
        #endregion

        #region Custom Methods

        #endregion


        #region Callbacks
        private void On_CollectableCollected(object args)
        {
            m_audioEventChannel.RaiseSFXPlayEvent(AudioId.CollectableSFX, 0.5f);
            PlayerResourcesManager.Give(ResourceID.KEDOS_ID);
        }

        #endregion

    }
}