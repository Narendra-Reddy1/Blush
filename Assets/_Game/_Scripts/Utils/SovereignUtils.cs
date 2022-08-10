using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public static class SovereignUtils
    {
        public static void Log(object message)
        {
            Debug.Log(message.ToString());
        }

        public static void LogError(object message)
        {
            Debug.LogError(message.ToString());
        }
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message.ToString());
        }
    }
}