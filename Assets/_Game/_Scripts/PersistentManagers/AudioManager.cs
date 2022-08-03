using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance { get; private set; }
        [SerializeField] private AudioFilesConfig m_audioFilesAsset;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgmSource;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
                instance = this;
        }

        public void PlayMusic(AudioId audioId)
        {
            bgmSource.clip = m_audioFilesAsset.GetAudioClip((int)audioId);
            bgmSource.loop = true;
            bgmSource.Play();
        }
        public void PlaySFX(AudioId audioId, float volume = 1)
        {
            sfxSource.volume = volume;
            sfxSource.PlayOneShot(m_audioFilesAsset.GetAudioClip((int)audioId));
        }
        public void StopMusic()
        {
            sfxSource.Stop();
        }
        public void StopBGM()
        {
            bgmSource.Stop();
        }
    }


    [System.Serializable]
    public struct AudioDictionary
    {
        public AudioId audioID;
        public AudioClip audioClip;
    }
    public enum AudioId
    {
        GamePlayBGM = 0,
        JumpSFX = 1,
        CollectableSFX = 2,
        JumpPadSFX = 3,
        BubblePopSFX = 4,
        MerchantTransitionSFX = 5,
        //UI
        UIButtonClickSFX = 6,
        UIButtonHoverSFX = 7,

    }
}