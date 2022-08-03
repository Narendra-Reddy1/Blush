using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "newAudioFilesAsset", menuName = "ScriptablesObjects/Audio/AudioFilesAsset")]
    public class AudioFilesConfig : BaseScriptableObject
    {
        //public bool Contains(AudioId audioId)
        //{
        //    bool isContain = false;
        //    foreach (AudioDictionary item in audioDictionay)
        //    {
        //        if (item.audioID == audioId && item.audioClip != null)
        //            isContain = true;
        //    }


        //    return isContain;
        //}
        //public List<AudioDictionary> audioDictionay;
        public List<AudioClip> audioClips;
        public AudioClip GetAudioClip(int index)
        {
            if (index > audioClips.Count) return null;
            return audioClips[index];
        }
    }
}
