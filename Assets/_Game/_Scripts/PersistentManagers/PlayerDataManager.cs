using System.Collections.Generic;
using UnityEngine;
using System;

namespace Naren_Dev
{
    public class PlayerDataManager : MonoBehaviour
    {
        #region SINGLETON
        public static PlayerDataManager instance { get; private set; }
        #endregion

        #region Variables

        private PlayerData m_playerData;
        public PlayerData playerData => m_playerData;

        private static bool m_isPlayerDataLoaded = false;
        public int levelKey { get; private set; }

        public static Func<bool> OnDataInitialized { get; private set; }
        #endregion

        #region Unity Built-In Methods

        private void Awake()
        {
            if (instance != this && instance != null)
            {
                Destroy(gameObject);
            }
            else if (instance == null)
            {
                instance = this;
            }
            SovereignUtils.Log($"Path: {Application.persistentDataPath}");
        }

        public void Start()
        {
            if (SessionTracker.SessionNumber == 1)
            {
                //First time game is being opened
                m_playerData = new PlayerData();
                //_AddFirstStartData();
            }
            else
            {
                //Load Data
                LoadData();
            }
            m_isPlayerDataLoaded = true;
            OnDataInitialized?.Invoke();
            //Save Data
            SaveData();
        }

        private void OnEnable()
        {
            OnDataInitialized += IsPlayerDataLoaded;
        }
        private void OnDisable()
        {
            OnDataInitialized -= IsPlayerDataLoaded;
        }
        #endregion

        #region Custom Methods
        public static bool IsPlayerDataLoaded()
        {
            return m_isPlayerDataLoaded;
        }

        /// <summary>
        /// This method is responsible for saving the player data.
        /// </summary>
        public void SaveData()
        {
            m_playerData.storeInventory = PlayerResourcesManager.StoreInventory;
            m_playerData.highestUnlockedLevel = GlobalVariables.HighestUnlockedLevel;
            DataSerializer.Save<PlayerData>("playerData.dat", m_playerData);
            SovereignUtils.Log($"Done with saving...");
        }

        /// <summary>
        /// This method is responsible for loading the player data.
        /// </summary>
        public void LoadData()
        {
            m_playerData = DataSerializer.Load<PlayerData>("playerData.dat");
            if (m_playerData == null)
            {
                Debug.Log(" Error with Loading..");
                m_playerData = new PlayerData();
                //_AddDefaultData();
                //_AddFirstStartData();
                SaveData();
                return;
            }
            PlayerResourcesManager.StoreInventory = m_playerData.storeInventory;
            GlobalVariables.HighestUnlockedLevel = m_playerData.highestUnlockedLevel;
            Debug.Log("Done with Loading");
        }


        //public void UpdateHighestUnlockedLevel()
        //{
        //    if (GlobalVariables.CurrentSelectedLevelIndex >= GlobalVariables.HighestUnlockedLevel)
        //    {
        //        GlobalVariables.HighestUnlockedLevel = GlobalVariables.CurrentSelectedLevelIndex + 1;
        //        GlobalVariables.HighestUnlockedLevelKey = GlobalVariables.HighestUnlockedLevel + "_0";
        //        Debug.Log($"HighestUnlockedLevel: {GlobalVariables.HighestUnlockedLevel}, HighestUnlockedLevelKey{GlobalVariables.HighestUnlockedLevelKey}");
        //        SaveData();
        //    }
        //}



        //private void _AddFirstStartData()
        //{
        //    if (PlayerResourceManager.Instance == null)
        //    {
        //        DeftouchUtils.LogWarning("Store Manager not found");
        //        Debug.Break(); // IF THIS BREAKS, FIGURE OUT A SOLUTION ON PRIORITY
        //    }
        //    PlayerResourceManager.Instance.InitializeStoreItems();
        //    PlayerResourceManager.Give(PlayerResourceManager.COINS_ITEM_ID, DeftouchConfig.DEFAULT_COIN_BALANCE);
        //    PlayerResourceManager.UpdateToPlayerData(PlayerResourceManager.COINS_ITEM_ID);
        //    PlayerResourceManager.Give(PlayerResourceManager.BUILD_CURRENCY_ITEM_ID, DeftouchConfig.DEFAULT_BUILD_CURRENCY_BALANCE);
        //    PlayerResourceManager.UpdateToPlayerData(PlayerResourceManager.BUILD_CURRENCY_ITEM_ID);

        //    PlayerResourceManager.Give(PlayerResourceManager.SHUFFLE_POWERUP_ITEM_ID, DeftouchConfig.DEFAULT_SHUFFLE_POWERUP_BALANCE);
        //    PlayerResourceManager.UpdateToPlayerData(PlayerResourceManager.SHUFFLE_POWERUP_ITEM_ID);

        //    PlayerResourceManager.Give(PlayerResourceManager.UNDO_POWERUP_ITEM_ID, DeftouchConfig.DEFAULT_UNDO_POWERUP_BALANCE);
        //    PlayerResourceManager.UpdateToPlayerData(PlayerResourceManager.UNDO_POWERUP_ITEM_ID);

        //    PlayerResourceManager.Give(PlayerResourceManager.RETUR3_POWERUP_ITEM_ID, DeftouchConfig.DEFAULT_RETURN3_POWERUP_BALANCE);
        //    PlayerResourceManager.UpdateToPlayerData(PlayerResourceManager.RETUR3_POWERUP_ITEM_ID);

        //    DeftouchAnalyticsCurrencyTracker.pInstance.UpdateCoins("welcome_bonus", DeftouchConfig.DEFAULT_COIN_BALANCE, false);
        //    DeftouchAnalyticsCurrencyTracker.pInstance.UpdateBuildCurrency("welcome_bonus", DeftouchConfig.DEFAULT_BUILD_CURRENCY_BALANCE, false);
        //    DeftouchAnalyticsCurrencyTracker.pInstance.UpdatePowerups("welcome_bonus", PowerUpType.Shuffle,
        //        DeftouchConfig.DEFAULT_SHUFFLE_POWERUP_BALANCE, false);
        //    DeftouchAnalyticsCurrencyTracker.pInstance.UpdatePowerups("welcome_bonus", PowerUpType.Undo,
        //        DeftouchConfig.DEFAULT_UNDO_POWERUP_BALANCE, false);
        //    DeftouchAnalyticsCurrencyTracker.pInstance.UpdatePowerups("welcome_bonus", PowerUpType.Return3,
        //        DeftouchConfig.DEFAULT_RETURN3_POWERUP_BALANCE, false);
        //}

        /// <summary>
        /// This method is used to set default values when player first time playing the game  or 
        /// if the saved file not found.
        /// </summary>
        //private void _AddDefaultData()
        //{
        //    PlayerResourceManager.Instance.coins.Give(9999);
        //    PlayerResourceManager.Instance.buildCurency.Give(9999);
        //}

        #endregion
    }

    /// <summary>
    /// This is persistant class to store the player data.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        //public string levelKey;
        public int highestUnlockedLevel;

        public Dictionary<ResourceID, VirtualItem> storeInventory;

        public PlayerData()
        {

            this.storeInventory = new Dictionary<ResourceID, VirtualItem>();
            this.highestUnlockedLevel = 0;
        }

    }
}