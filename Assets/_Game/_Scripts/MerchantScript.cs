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
        [SerializeField] private Flowchart m_cutSceneChart;

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

        private async void ShowCutScene()
        {
            m_spriteRenderer.enabled = true;
            m_audioEventChannel.RaiseSFXPlayEvent(AudioId.MerchantTransitionSFX);
            m_merchantAnim.enabled = true;
            //  m_merchantAnim.SetTrigger("canAppear");
            GetComponent<Collider2D>().enabled = false;
            await System.Threading.Tasks.Task.Delay(1700);//Time to complete Merchant appearing effect.
            m_cutSceneChart.ExecuteBlock("START");

        }
        private void EndCutScene()
        {
            _Disappear();
            m_merchantAnim.enabled = false;
            m_spriteRenderer.enabled = false;
            m_cutSceneChart.enabled = false;

        }
        public void RewardPlayer(ColorID colorID)
        {
            UIManager.instance.ShowUnlockColorAnimation(colorID);
            SovereignUtils.Log($"Unlocking Color: {colorID}");
        }
        public void TriggerEndCutScene()
        {
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