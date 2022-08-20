using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naren_Dev
{
    public class SessionTracker : MonoBehaviour
    {

        public static SessionTracker instance;

        public static int SessionNumber
        {
            get; set;
        }
        //public static string SessionId
        //{
        //    get; set;
        //}

        public static long SessionStartTime
        {
            get; set;
        }

        private WaitUntil waitForDataInitialized = new WaitUntil(PlayerDataManager.OnDataInitialized);

        // Use this for initialization
        void Awake()
        {
            Debug.LogWarning(this.GetType().Name + "  " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            #region SingletonLogic
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
            #endregion

            Time.timeScale = 1;

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                //UpdateSessionId();
                if (PlayerPrefsWrapper.HasKey(PlayerPrefKeys.SessionNumber))
                {
                    //Not the first game
                    SessionNumber = PlayerPrefsWrapper.GetPlayerPrefsInt(PlayerPrefKeys.SessionNumber) + 1;
                    SaveSessionNumber();
                }
                else
                {
                    SessionNumber = 1;
                    SaveSessionNumber();
                    // StartCoroutine(DelayedInstallEvent());
                }
            }

            //Setting the main menu session counter as 0, means it is a fresh session
            PlayerPrefsWrapper.SetPlayerPrefsInt(PlayerPrefKeys.MainMenuSessionCounter, 0);

            //DeftEventHandler.AddListener(EventID.EVENT_ON_GAME_MAXIMISED, HandleGameMaximised);
            //DeftEventHandler.AddListener(EventID.EVENT_ON_GAME_MAXIMISED, HandleGameMinimised);
          //  StartCoroutine(WaitAndUpdate());
            Debug.Log("Current Session Number: " + SessionNumber);
            //PlayerPrefs.DeleteKey("SessionNumber");
        }

        private IEnumerator WaitAndUpdate()
        {
            yield return waitForDataInitialized;
        }

        private void HandleGameMinimised(object arg)
        {
            ApplicationPause(true);
        }

        private void HandleGameMaximised(object arg)
        {
            ApplicationPause(false);
        }

        private void SaveSessionNumber()
        {
            PlayerPrefsWrapper.SetPlayerPrefsInt(PlayerPrefKeys.SessionNumber, SessionNumber);
            PlayerPrefsWrapper.SavePlayerPrefs();
            //PlayerPrefs.SetInt("SessionNumber", SessionNumber);
            //PlayerPrefs.Save();
        }

        //public void UpdateSessionId()
        //{
        //    if (string.IsNullOrEmpty(SessionId))
        //    {
        //        SessionId = SystemInfo.deviceUniqueIdentifier + "_" + SovereignUtils.GetEpochTime();
        //        SessionStartTime = SovereignUtils.GetEpochTime();
        //    }
        //}

        private void ApplicationPause(bool pause)
        {

        }


        private void OnApplicationQuit()
        {
            Debug.Log("Event Trigger RecordSessionCompleted , session_ended  ");
        }

        private void OnDestroy()
        {
            //GlobalEventHandler.RemoveListener(EventID.EVENT_ON_GAME_MAXIMISED, HandleGameMaximised);
            //GlobalEventHandler.RemoveListener(EventID.EVENT_ON_GAME_MAXIMISED, HandleGameMinimised);
            StopAllCoroutines();
        }

        //private IEnumerator DelayedInstallEvent()
        //{
        //    Debug.Log(" DelayedInstallEvent()");
        //    yield return new WaitUntil(() => ThirdPartyInitializer.isAnalyticsReady);
        //}
    }
}