using UnityEngine;


namespace Naren_Dev
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables

        //  public static AudioManager instance { get; private set; }
        [SerializeField] private AudioFilesConfig m_audioFilesAsset;
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel = default;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgmSource;
        private bool m_musicToggle = true;
        private bool m_sfxToggle = true;

        #endregion Variables

        #region Unity Methods

        //private void Awake()
        //{
        //    if (instance != null && instance != this)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //    else
        //        instance = this;
        //}
        private void OnEnable()
        {
            m_audioEventChannel.OnMusicPlayRequested += _PlayMusic;
            m_audioEventChannel.OnMusicStopRequested += _StopMusic;
            m_audioEventChannel.OnSFXPlayRequested += _PlaySFX;
            m_audioEventChannel.OnSFXStopRequested += _StopSFX;
            m_audioEventChannel.OnAudioToggleRequested += _SetAudioPreferences;
        }
        private void OnDisable()
        {
            m_audioEventChannel.OnMusicPlayRequested -= _PlayMusic;
            m_audioEventChannel.OnMusicStopRequested -= _StopMusic;
            m_audioEventChannel.OnSFXPlayRequested -= _PlaySFX;
            m_audioEventChannel.OnSFXStopRequested -= _StopSFX;
            m_audioEventChannel.OnAudioToggleRequested -= _SetAudioPreferences;
        }

        #endregion Unity Methods

        #region Custom Methods

        private void _Init()
        {
            bgmSource.outputAudioMixerGroup = m_audioFilesAsset.bgmMixerGroup;
            sfxSource.outputAudioMixerGroup = m_audioFilesAsset.sfxMixerGroup;
        }
        /// <summary>
        /// Takes AudioID as parameter and fetches for the audioClip sfx and bgm dictionaries
        /// </summary>
        /// <param name="audioId"></param>
        private void _PlayMusic(AudioId audioId, float volume, bool canLoop = false)
        {
            bgmSource.clip = m_audioFilesAsset.GetBgmAudioClip(audioId);
            bgmSource.loop = true;
            bgmSource.Play();
        }
        /// <summary>
        /// Takes AudioID as parameter and fetches for the audioClip from sfx and bgm dictionaries
        /// </summary>
        /// <param name="audioId"></param>
        private void _PlaySFX(AudioId audioId, float volume = 1)
        {
            sfxSource.volume = volume;
            sfxSource.PlayOneShot(m_audioFilesAsset.GetSFXAudioClip(audioId));
        }
        private void _StopSFX()
        {
            sfxSource.Stop();
        }
        private void _StopMusic()
        {
            bgmSource.Stop();
        }
        private void _SetAudioPreferences(bool status, AudioStatus audioStatus)
        {
            switch (audioStatus)
            {
                case AudioStatus.MusicStatus:
                    PlayerPrefsWrapper.SetPlayerPrefsBool(PlayerPrefKeys.MusicStatus, status);
                    break;
                case AudioStatus.SFXStatus:
                    PlayerPrefsWrapper.SetPlayerPrefsBool(PlayerPrefKeys.SFXStatus, status);
                    break;
            }
        }
    }
    #endregion Custom Methods
}