using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Naren_Dev
{
    public static class SovereignUtils
    {
      
        public static string ShortenString(string input, int length)
        {
            return (input.Length < length) ? input : input.Substring(0, length);
        }

        public static Color ColorRGB(float r, float g, float b)
        {
            r = r / 255;
            g = g / 255;
            b = b / 255;
            return new Color(r, g, b, 1);
        }

        public static Color ColorRGBA(float r, float g, float b, float a)
        {
            r = r / 255;
            g = g / 255;
            b = b / 255;
            a = a / 255;
            return new Color(r, g, b, a);
        }

        public static int RoundToNearestTen(int number)
        {
            if (number % 10 == 0)
            {
                return number;
            }
            else
            {
                int excess = number % 10; //this will give the last digit
                if (excess < 5)
                {
                    number -= excess;
                }
                else
                {
                    number += (10 - excess);
                }
                return number;
            }
        }

        public static int RoundToUpwardsHundered(int number)
        {
            if (number % 100 == 0)
            {
                return number;
            }
            else
            {
                int excess = number % 100; //this will give the last 2 digits
                number += (100 - excess);
                return number;
            }
        }
        public static void LogError(object s)
        {
#if UNITY_EDITOR
            Debug.LogError(s);
#else
        if (Debug.isDebugBuild)
            Debug.LogWarning(s);
#endif
        }
        public static void Log(object s)
        {
#if UNITY_EDITOR
            Debug.Log(s);
#else
        //if (Debug.isDebugBuild)
            Debug.Log(s);
#endif
        }

        public static void LogWarning(object s)
        {
#if UNITY_EDITOR
            Debug.LogWarning(s);
#else
        if (Debug.isDebugBuild)
            Debug.LogWarning(s);
#endif
        }

        public static int NetworkTypeConnection()
        {
            switch (Application.internetReachability)
            {
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    return 1; //carrier Network
                case NetworkReachability.ReachableViaLocalAreaNetwork:

                    return 0;//Local or Wifi

            }
            return -1;
        }
        public static bool IsNetworkAvailable()
        {
            switch (Application.internetReachability)
            {
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                case NetworkReachability.ReachableViaLocalAreaNetwork:

                    return true;

            }
            return false;
        }


        /// <summary>
        /// Get time in datetime format
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetTimeAsDateTime(string time)
        {
            return GetTimeAsDateTime(Convert.ToInt64(time));
        }
        /// <summary>
        /// Get time in datetime format
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetTimeAsDateTime(long time)
        {
            DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(time);
            return
                dtDateTime;  // NOTE - We can return as Local time but at no place are we using absolute values, we always calculate time left so we can return it without converting to Local Time - .ToLocalTime();
        }
        public static T[] ShuffleArray<T>(T[] arr)
        {
            for (int i = arr.Length - 1; i > 0; i--)
            {
                int r = Random.Range(0, i);
                T tmp = arr[i];
                arr[i] = arr[r];
                arr[r] = tmp;
            }
            return arr;
        }
        public static long GetEpochTime(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static long GetEpochTime()
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Local);
            int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            return cur_time;
        }

        public static double GetMinutesTillHour(int hour)
        {
            DateTime nextTime;
            int currentTimeHour = DateTime.Now.Hour;
            if (hour > currentTimeHour)
            {
                nextTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
                return (nextTime - DateTime.Now).TotalMinutes;
            }
            else if (currentTimeHour >= hour) //If this condition is true then the next hour is on a new day
            {
                nextTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
                nextTime.AddDays(1);
                return (nextTime - DateTime.Now).TotalMinutes;
            }

            return 0;
        }

        public static List<T> RandomizeList<T>(List<T> list)
        {
            List<T> randomizedList = new List<T>();
            System.Random rnd = new System.Random();

            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count);
                randomizedList.Add(list[index]);
                list.RemoveAt(index);
            }
            return randomizedList;
        }

        /// <summary>
        /// Removes ',' from a string. This is done to prevent from format exception while parsing to int.
        /// </summary>
        public static Int32 ParseToInt(string numString)
        {
            numString = numString.Replace(",", "");
            return Int32.Parse(numString);
        }
        public static string GetEnumString<T>(string enumString)
        {
            var field = typeof(T).GetField(enumString);
            var attr = (EnumMemberAttribute)field.GetCustomAttributes(typeof(EnumMemberAttribute), false).SingleOrDefault();
            return attr.Value;
        }



    }
}