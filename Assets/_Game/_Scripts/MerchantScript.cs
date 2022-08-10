using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class MerchantScript : MonoBehaviour
    {
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel;
        [SerializeField] private Animator m_merchantAnim;

        private void Awake()
        {
            if (m_merchantAnim == null)
                m_merchantAnim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                m_audioEventChannel.RaiseSFXPlayEvent(AudioId.MerchantTransitionSFX);
                //  AudioManager.instance.PlaySFX(AudioId.MerchantTransitionSFX);
                GetComponent<Collider2D>().enabled = false;
                UIManager.instance.UnlockColor(1, true);
                UIManager.instance.UnlockColor(2, true);
                UIManager.instance.UnlockColor(1, false);
                UIManager.instance.UnlockColor(2, false);

                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial_1", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                // InputManager.instance.hasControlAcces = false;
                m_merchantAnim.SetTrigger("canAppear");
                Invoke(nameof(_Disappear), 1.5f);
            }
        }
        private void _Disappear()
        {
            m_merchantAnim.SetTrigger("Disappear");
            Invoke(nameof(_Disappear), 2f);
        }
    }
}