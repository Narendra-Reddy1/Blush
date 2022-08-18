using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "newAudioFilesAsset", menuName = "ScriptablesObjects/Audio/AudioFilesAsset")]
    public class AudioFilesConfig : BaseScriptableObject
    {
        [Header("==================SFX Audio Clips==================")]
        public AudioDictionary sfxAudioDictionary;
        [Space(5)]
        [Header("==================BGM Audio Clips==================")]
        public AudioDictionary bgmAudioDictionary;

        [Space(10)]
        public AudioMixerGroup sfxMixerGroup;
        public AudioMixerGroup bgmMixerGroup;
        public AudioMixerGroup masterMixerGroup;

        public List<AudioClip> audioClips;
        public AudioClip GetSFXAudioClip(AudioId audioId)
        {
            sfxAudioDictionary.TryGetValue(audioId, out AudioClip audio);
            return audio;
        }
        public AudioClip GetBgmAudioClip(AudioId audioId)
        {
            bgmAudioDictionary.TryGetValue(audioId, out AudioClip audio);
            return audio;
        }
    }

    [System.Serializable]
    public class AudioClipReference : AssetReferenceT<AudioClip>
    {
        public AudioClipReference(string guid) : base(guid)
        {

        }
    }

}
