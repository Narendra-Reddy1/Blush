using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Naren_Dev
{
    public class Collectable : MonoBehaviour
    {
        #region Variables

        public bool IsGem => m_isGem;
        private Transform m_transform;
        [SerializeField] private Vector3 m_punchScale = new Vector3(0.01f, 0.01f, 0.01f);
        [SerializeField] private float m_tweeningSpeed = 0.25f;
        [SerializeField] private bool m_isGem = true;
        private Transform targetTrans;
        private bool m_tween = false;

        #endregion

        #region Unity Built-In Methods


        private void Awake()
        {
            m_transform = transform;
        }
        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_COLLECTABLE_COLLECTED, Callback_On_Collectable_Collected);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_COLLECTABLE_COLLECTED, Callback_On_Collectable_Collected);
        }

        private void Update()
        {
            if (m_tween)
            {
                PlayTweening();
            }
        }

        #endregion

        #region Custom Methods

        public void PlayTweening()
        {
            m_transform.DOMove(targetTrans.position, m_tweeningSpeed * 2.5f).OnComplete(() => { _OnTweenComplete(); });
        }
        public void ShowCollectableEffect(Transform target)
        {
            targetTrans = target;
            m_tween = true;
        }
        private void _OnTweenComplete()
        {
            m_transform.DOScale(Vector3.zero, m_tweeningSpeed).OnComplete(() => { gameObject.SetActive(false); });

        }

        #endregion
        #region Callbacks
        private void Callback_On_Collectable_Collected(object args)
        {
            var arr = (Transform[])args;
            if (arr[1] != this.transform)
                return;
            Transform target = arr[0];
            ShowCollectableEffect(target);
        }

        #endregion

    }

}