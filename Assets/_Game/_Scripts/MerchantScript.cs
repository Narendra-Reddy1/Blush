using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
namespace Naren_Dev
{
    public class MerchantScript : MonoBehaviour, IInitializer
    {
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel;
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private Animator m_merchantAnim;
        [SerializeField] private CutSceneID m_cutSceneID;

        private void Awake()
        {
            Init();
        }
        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_CUTSCENE_ENDED, Callback_On_CutScene_Ended);
            GlobalEventHandler.AddListener(EventID.EVENT_ON_CUTSCENE_STARTED, Callback_On_CutScene_Started);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_CUTSCENE_ENDED, Callback_On_CutScene_Ended);
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_CUTSCENE_STARTED, Callback_On_CutScene_Started);
        }

        public void Init()
        {
            if (m_merchantAnim == null)
                m_merchantAnim = GetComponent<Animator>();
            m_spriteRenderer.enabled = false;
        }

        private void ShowCutScene()
        {
            m_spriteRenderer.enabled = true;
            m_audioEventChannel.RaiseSFXPlayEvent(AudioId.MerchantTransitionSFX);
            m_merchantAnim.enabled = true;
            //  m_merchantAnim.SetTrigger("canAppear");
            GetComponent<Collider2D>().enabled = false;
            GameManager.instance.StartNarration(m_cutSceneID.ToString());

        }
        private void EndCutScene()
        {
            _Disappear();
            m_merchantAnim.enabled = false;
            m_spriteRenderer.enabled = false;
            //  m_cutSceneChart.enabled = false;
            GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_GAMESTATE_CHANGED, GameState.GamePlay);

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_GAMESTATE_CHANGED, GameState.CutScene);
            }
        }
        private void _Disappear()
        {
            m_merchantAnim.SetTrigger("Disappear");
            //Invoke(nameof(_Disappear), 2f);
        }
        private void Callback_On_CutScene_Started(object args)
        {
            ShowCutScene();
        }
        private void Callback_On_CutScene_Ended(object args)
        {
            EndCutScene();
        }

    }
}