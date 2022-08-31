using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Naren_Dev
{
    public enum AnimationDirection
    {
        Up = 1,
        Down = -1
    }
    public class AnimatedColorPiece : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject m_target;
        [SerializeField] private Image m_colorPiece;

        [SerializeField] private AnimationCurve rewardAnimationCurve;
        [SerializeField] private float moveDuration = 0.2f;
        [SerializeField] private float animationHeight = 100;
        #endregion Variables

        #region UnityMethods
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                SovereignUtils.Log($"Moving To Target...");
                StartCoroutine(MoveToTarget(m_colorPiece.transform, m_colorPiece.GetComponent<RectTransform>().localPosition, m_target.GetComponent<RectTransform>().localPosition, AnimationDirection.Up, rewardAnimationCurve, () => { }));
            }
        }
        #endregion UnityMethods

        #region Custom Methods
        public void Init(Color pieceColor)
        {
            m_colorPiece.color = pieceColor;
        }

        private IEnumerator MoveToTarget(Transform animatingObject, Vector3 start, Vector3 end, AnimationDirection direction, AnimationCurve animationCurve, Action onComplete)
        {
            //yield return new WaitForSeconds(0.3f);
            SovereignUtils.Log($"Moving To Target...");
            float time = 0f;
            while (time <= moveDuration)
            {
                time += Time.deltaTime;

                float linearT = time / moveDuration;
                float heightT = animationCurve.Evaluate(linearT);

                float height = Mathf.Lerp(0f, (int)direction * animationHeight, heightT);

                animatingObject.localPosition = Vector3.Lerp(start, end, linearT) + new Vector3(0f, height, 0f);

                yield return null;
            }
            onComplete.Invoke();
        }
        #endregion Custom Methods


        #region Callbacks

        #endregion Callbacks
    }
}