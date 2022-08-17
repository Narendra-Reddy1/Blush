using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    [Serializable]
    public class AudioDictionary : SerializableDictionary<AudioId, AudioClip> { }


#if NET_4_6 || NET_STANDARD_2_0
    [Serializable]
    public class StringHashSet : SerializableHashSetBase.SerializableHashSet<string> { }

#endif
}
