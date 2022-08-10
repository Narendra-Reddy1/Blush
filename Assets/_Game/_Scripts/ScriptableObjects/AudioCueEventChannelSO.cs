using System;
using UnityEngine;


namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "newAudioEventChannel", menuName = "ScriptablesObjects/Audio/Events/AudioEventChannel")]
    public class AudioCueEventChannelSO : BaseScriptableObject
    {
        public Action<AudioId, float, bool> OnMusicPlayRequested = default;
        public Action<AudioId, float> OnSFXPlayRequested = default;
        public Action OnMusicStopRequested = default;
        public Action OnSFXStopRequested = default;
        // public AudioCueFinishAction OnAudioFinishRequested;
        public Action<bool, AudioStatus> OnAudioToggleRequested;

        /// <summary>
        /// This method responsible to play given sfx and volume.
        /// </summary>
        /// <param name="audioId"></param>
        /// <param name="volume"></param>
        public void RaiseSFXPlayEvent(AudioId audioId, float volume = 1)
        {
            try
            {
                OnSFXPlayRequested?.Invoke(audioId, volume);
            }
            catch (System.Exception e)
            {
                SovereignUtils.LogError($"Error(s) from OnSFXPlayRequested Event: {e.Message}");
            }
        }

        /// <summary>
        /// This method is responsible to play given music with properties like volume, loop.
        /// </summary>
        /// <param name="audioId"></param>
        /// <param name="volume"></param>
        /// <param name="canLoop"></param>
        public void RaiseMusicPlayEvent(AudioId audioId, float volume = 1, bool canLoop = true)
        {
            try
            {
                OnMusicPlayRequested.Invoke(audioId, volume, canLoop);
            }
            catch (System.Exception e)
            {
                SovereignUtils.LogError($"Error(s) from OnMusicPlayRequested Event: {e.Message}");
            }
        }

        /// <summary>
        /// This method stops the currently playing SFX.
        /// </summary>
        public void RaiseSFXStopEvent()
        {
            try
            {
                OnSFXStopRequested.Invoke();
            }
            catch (System.Exception e)
            {
                SovereignUtils.LogError($"Error(s) from OnSFXStopRequested Event: {e.Message}");
            }
        }

        /// <summary>
        /// This method stops currently playing music
        /// </summary>
        public void RaiseMusicStopEvent()
        {
            try
            {
                OnMusicStopRequested.Invoke();
            }
            catch (System.Exception e)
            {
                SovereignUtils.LogError($"Error(s) from OnMusicStopRequested Event: {e.Message}");
            }
        }

        public void RaiseAudioToggleEvent(bool toggle, AudioStatus audioStatus)
        {
            try
            {
                OnAudioToggleRequested.Invoke(toggle, audioStatus);
            }
            catch (Exception e)
            {
                SovereignUtils.LogError($"Error(s) from AudioToggle Event: {e.Message}");
            }
        }

        //public void RaiseAudioFinishEvent()
        //{

        //}


    }
    [Serializable]
    public class AssetReferenceAudioEventChannel : UnityEngine.AddressableAssets.AssetReferenceT<AudioCueEventChannelSO>
    {
        public AssetReferenceAudioEventChannel(string guid) : base(guid) { }
    }
}