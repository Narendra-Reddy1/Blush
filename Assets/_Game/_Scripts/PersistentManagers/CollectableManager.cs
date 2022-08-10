using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Naren_Dev
{
    public class CollectableManager : MonoBehaviour
    {
        #region Singleton

        public static CollectableManager instance { get; private set; }

        #endregion

        #region UnityEvents

        public UnityEvent<Collectable, Transform> onCollectableCollected = null;

        #endregion

        #region Variables
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel;
        [SerializeField] private int m_collectablesCollected = 0;
        [SerializeField] private int m_effectSize = 4;
        [SerializeField] private GameObject m_collectableParticlePrefab;
        private List<GameObject> m_collectableParticleList;

        #endregion

        #region Unity Built-In Methods

        private void OnEnable()
        {
            onCollectableCollected.AddListener(OnCollectableCollected);
        }
        private void OnDisable()
        {
            onCollectableCollected.RemoveListener(OnCollectableCollected);
        }

        private void Awake()
        {
            if (instance != null && instance != this) Destroy(this.gameObject);
            else if (instance == null)
                instance = this;
        }
        private void Start()
        {
            /*ObjectPooler.SpawnObjects(m_collectableParticlePrefab,)*/
        }

        #endregion

        #region Custom Methods



        public void OnCollectableCollected(Collectable collectable, Transform target)
        {
            if (collectable.IsGem)
            {/*
#if UNITY_EDITOR
                collectable.gameObject.SetActive(false);
#else
                Destroy(collectable.gameObject);
#endif*/
                //AudioManager.instance?.PlaySFX(AudioId.CollectableSFX, 0.5f);
                m_audioEventChannel.RaiseSFXPlayEvent(AudioId.CollectableSFX, 0.5f);
                collectable.ShowCollectableEffect(target);

                m_collectablesCollected++;
            }
        }


        #endregion

    }
}