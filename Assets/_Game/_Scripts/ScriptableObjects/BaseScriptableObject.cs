using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class BaseScriptableObject : ScriptableObject
    {
        [TextArea(4, 10)] public string description;
    }
}