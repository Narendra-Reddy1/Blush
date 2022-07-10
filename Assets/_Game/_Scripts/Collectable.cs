using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fungus;

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
            m_transform.DOMove(targetTrans.position, m_tweeningSpeed*2.5f).OnComplete(() => { _OnTweenComplete(); });
            //m_transform.position = Vector3.Lerp(m_transform.position, targetTrans.position, m_tweeningSpeed * Time.deltaTime);
        }
        public void ShowCollectableEffect(Transform target)
        {
          //  m_transform.DOPunchScale(m_punchScale, m_tweeningSpeed);

            targetTrans = target;
            m_tween = true;

           // StartCoroutine(FollowPlayer(target));
           // Debug.Log("Before Move is called");
           // Move m = new Move(gameObject, target, flowchart);
          //  m.Play();
            //flowchart.AddSelectedCommand(MoveTo.)
            //m_transform.DOMove(target.position,5f);
            //m_transform.position= Vector3.Lerp(m_transform.position, target.position, m_tweeningSpeed * Time.deltaTime);
        }
        #endregion
        private void _OnTweenComplete()
        {
            //m_tween = false;
            m_transform.DOScale(Vector3.zero, m_tweeningSpeed).OnComplete(()=> { gameObject.SetActive(false); });
            
        }
        #region Coroutines
        /*private IEnumerator FollowPlayer(Transform target)
        {
            yield return null;
            m_transform.position=Vector3.Lerp(m_transform.position,target.position,m_tweeningSpeed*Time.deltaTime);
        }*/
        #endregion

    }

}/*
class Move : MoveTo
{
    Flowchart flowchart;
    public Move(GameObject target, Transform to,Flowchart flowchart)
    {
        Debug.Log("Inside Move Constructor");
         _targetObject.gameObjectVal = target;
        _toTransform.transformVal = to;
        this.flowchart = flowchart;
    }

    public void Play()
    {
        Debug.Log("Inside Play");
        flowchart.ExecuteBlock("MoveTo");
        Block b = new Block();
    }
}*/