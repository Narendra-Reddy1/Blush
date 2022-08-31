using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Player/LevelData")]
    public class LevelData : BaseScriptableObject
    {
        public LevelDictionary levelsCollection;

        
    }
}