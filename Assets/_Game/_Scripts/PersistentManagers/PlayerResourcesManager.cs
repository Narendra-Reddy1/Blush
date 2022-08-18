using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    [System.Serializable]
    public class PlayerResourcesManager : MonoBehaviour
    {
        public static PlayerResourcesManager Instance;
        public static bool debugMode = true;

        bool isStoreManagerInitialized = false;

        private void Awake()
        {
            // if the singleton hasn't been initialized yet
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            Instance = this;
            //DontDestroyOnLoad (this.gameObject);
        }

        void Start()
        {
            InitializeStoreItems();
            updateValueStoreItems();
        }

        public static string COINS_ITEM_ID = "COIN_CURRENCY";
        public static string BUILD_CURRENCY_ITEM_ID = "BUILD_CURRENCY";
        public static string UNDO_POWERUP_ITEM_ID = "UNDO_POWERUP";
        public static string RETUR3_POWERUP_ITEM_ID = "RETUR3_POWERUP";
        public static string SHUFFLE_POWERUP_ITEM_ID = "SHUFFLE_POWERUP";


        [SerializeField]
        public static Dictionary<string, VirtualItem> StoreInventory;

        [Header("Currencies")]
        public Currency coins;
        public Currency buildCurency;
        //public PowerUp undoPowerUp;
        //public PowerUp return3PowerUp;
        //public PowerUp shufflePowerUp;

        /*	[Header ("Consumable")]
            public Consumable bounceAssist;
            public Consumable superShot;
            public Consumable swingAssist;*/

        public void updateValueStoreItems()
        {
            //((Consumable)StoreInventory [BOUNCE_ASSIST_ITEM_ID]).SetPurchaseValue(200);
            //PlayerDataManager.instance.SaveData ();
        }

        /// <summary>
        /// Initializes the store items.
        /// Call this Method the very first time the store is used
        /// </summary>
        public void InitializeStoreItems()
        {
            if (StoreInventory == null)
            {
                StoreInventory = new Dictionary<string, VirtualItem>();
            }
            isStoreManagerInitialized = true;

            //Currency define - Start everything with zero balance..aditional items can be given later
            coins = new Currency(COINS_ITEM_ID);
            buildCurency = new Currency(BUILD_CURRENCY_ITEM_ID);
            //undoPowerUp = new PowerUp(UNDO_POWERUP_ITEM_ID, COINS_ITEM_ID, DeftouchConfig.UNDO_POWERUP_COST);
            //return3PowerUp = new PowerUp(RETUR3_POWERUP_ITEM_ID, COINS_ITEM_ID, DeftouchConfig.RETURN3_POWERUP_COST);
            //shufflePowerUp = new PowerUp(SHUFFLE_POWERUP_ITEM_ID, COINS_ITEM_ID, DeftouchConfig.SHUFFLE_POWERUP_COST);
            //Add the item to the inventory.
            addItemToStoreInventory(coins);
            addItemToStoreInventory(buildCurency);
            //addItemToStoreInventory(undoPowerUp);
            //addItemToStoreInventory(return3PowerUp);
            //addItemToStoreInventory(shufflePowerUp);
        }

        void addItemToStoreInventory(VirtualItem item)
        {
            if (!StoreInventory.ContainsKey(item.itemId))
                StoreInventory.Add(item.itemId, item);
        }

        public void LoadDataForInspector()
        {
            //Currency
            coins = (Currency)StoreInventory[COINS_ITEM_ID];
            buildCurency = (Currency)StoreInventory[BUILD_CURRENCY_ITEM_ID];
        }

        /// <summary>
        /// Gets the item balance -
        /// Returns Balance for Currency, Consumable, Equipable
        /// Returns Current Level for Upgradable
        /// </summary>
        /// <returns>The item balance.</returns>
        public static int GetBalance(string itemId)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {

                case VirtualItemType.CURRENCY:
                    return ((Currency)StoreInventory[itemId]).GetBalance();

                //case VirtualItemType.CONSUMABLE:
                //    return ((Consumable)StoreInventory[itemId]).GetBalance();

                //case VirtualItemType.EQUIPABLE:
                //    return ((Equipable)StoreInventory[itemId]).GetBalance();

                //case VirtualItemType.UPGRADABLE:
                //    return ((Upgradable)StoreInventory[itemId]).GetBalance();

                //case VirtualItemType.POWERUP:
                //    return ((PowerUp)StoreInventory[itemId]).GetBalance();

                default:
                    Debug.Log("Invalid Item ID");

                    return -1;
            }



            //if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
            //{
            //    return ((Currency)StoreInventory[itemId]).GetBalance();
            //}
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
            //{
            //    return ((Consumable)StoreInventory[itemId]).GetBalance();
            //}
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
            //{
            //    return ((Equipable)StoreInventory[itemId]).GetBalance();
            //}
            //else
            //{
            //    //UPGRADABLE
            //    return ((Upgradable)StoreInventory[itemId]).GetBalance();
            //}
        }

        public static int GetCollectedBalance(string itemId)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {

                case VirtualItemType.CURRENCY:
                    return ((Currency)StoreInventory[itemId]).GetCollectedBalance();
                //case VirtualItemType.POWERUP:
                //    return ((PowerUp)StoreInventory[itemId]).GetCollectedBalance();

                default:
                    Debug.Log("Invalid Item ID");

                    return -1;
            }
        }

        public static int GetNonCollectedBalance(string itemId)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {

                case VirtualItemType.CURRENCY:
                    return ((Currency)StoreInventory[itemId]).GetNonCollectedBalance();
                //case VirtualItemType.POWERUP:
                //    return ((PowerUp)StoreInventory[itemId]).GetNonCollectedBalance();

                default:
                    Debug.Log("Invalid Item ID");

                    return -1;
            }
        }



        /// <summary>
        /// This method gives the coin balance .
        /// </summary>
        /// <returns>Coin Balance</returns>
        public int GetCoinBalance()
        {
            return GetBalance(COINS_ITEM_ID);
        }


        /// <summary>
        /// This method gives the build currency balance .
        /// </summary>
        /// <returns>Build Currency Balance</returns>
        public int GetBuildCurrencyBalance() => GetBalance(BUILD_CURRENCY_ITEM_ID);




        /// <summary>
        /// This Method Adds the given amount of coins .
        /// </summary>
        /// <param name="quantity"></param>
        public void AddCoins(int quantity)
        {
            Give(COINS_ITEM_ID, quantity);
            PlayerDataManager.instance.SaveData();
        }

        /// <summary>
        /// This method adds the given amount of currency.
        /// </summary>
        /// <param name="quantity"></param>
        public void AddBuildCurrency(int quantity)
        {
            Give(BUILD_CURRENCY_ITEM_ID, quantity);
            PlayerDataManager.instance.SaveData();
        }

        /// <summary>
        /// This method removes the given amount of coins.
        /// If the  balance is less than 0 it will resets to 0.
        /// </summary>
        /// <param name="quantity"></param>
        public void DeductCoins(int quantity)
        {
            Take(COINS_ITEM_ID, quantity);
            PlayerDataManager.instance.SaveData();
        }


        /// <summary>
        /// This method removes the given amount of currency.
        /// If the  balance is less than 0 it will resets to 0.
        /// </summary>
        /// <param name="quantity"></param>
        public void DeductBuildCurrency(int quantity)
        {
            Take(BUILD_CURRENCY_ITEM_ID, quantity);
            PlayerDataManager.instance.SaveData();
        }



        public static void ResetBalance(string itemId)
        {
            if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
            {
                ((Currency)StoreInventory[itemId]).ResetBalance();
            }
            PlayerDataManager.instance.SaveData();
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
            //{
            //	 ((Consumable)StoreInventory[itemId]).ResetBalance();
            //}
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
            //{
            //	return ((Equipable)StoreInventory[itemId]).ResetBalance();
            //}
            //else
            //{
            //	//UPGRADABLE
            // ((Currency)StoreInventory[itemId]).GetBalance();
            //}
        }
        public static void Take(string itemId)
        {
            Take(itemId, 1);
        }

        public static void Take(string itemId, int quantity)
        {
            if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
            {
                ((Currency)StoreInventory[itemId]).Take(quantity);
            }
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
            //{
            //    ((Consumable)StoreInventory[itemId]).Take(quantity);
            //}
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
            //{
            //    ((Equipable)StoreInventory[itemId]).Take(quantity);
            //}
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.POWERUP)
            //{
            //    ((PowerUp)StoreInventory[itemId]).Take(quantity);

            //}
            else
            {
                //UPGRADABLE - NOT APPLICABLE
            }
            if (debugMode)
            {
                Debug.Log("Take - " + itemId + " - " + quantity + "\nCurrent Balance - " + PlayerResourcesManager.GetBalance(itemId));
            }
            PlayerDataManager.instance.SaveData();
            invokeStoreGiveCallback();
        }

        public static void Give(string itemId)
        {
            Give(itemId, 1);
        }

        public static void Give(string itemId, int quantity)
        {
            if (debugMode)
            {
                Debug.Log("BEfore Give - " + itemId + " - " + quantity + "\nCurrent Balance - " + PlayerResourcesManager.GetBalance(itemId));
            }

            switch (StoreInventory[itemId].virtualItemType)
            {
                case VirtualItemType.CURRENCY:
                    ((Currency)StoreInventory[itemId]).Give(quantity);
                    break;

                //case VirtualItemType.CONSUMABLE:
                //    ((Consumable)StoreInventory[itemId]).Give(quantity);
                //    break;

                //case VirtualItemType.EQUIPABLE:
                //    ((Equipable)StoreInventory[itemId]).Give(quantity);
                //    break;

                //case VirtualItemType.POWERUP:
                //    ((PowerUp)StoreInventory[itemId]).Give(quantity);
                //    break;

                default:
                    Debug.Log("Invalid Item ID");
                    break;
            }

            PlayerDataManager.instance.SaveData();
            invokeStoreGiveCallback();
        }

        public static void UpdateToPlayerData(string itemId)
        {

            switch (StoreInventory[itemId].virtualItemType)
            {
                case VirtualItemType.CURRENCY:
                    ((Currency)StoreInventory[itemId]).UpdateBalance();
                    break;
                    //case VirtualItemType.POWERUP:
                    //    ((PowerUp)StoreInventory[itemId]).UpdateBalance();
                    //    break;
            }

            PlayerDataManager.instance.SaveData();
            invokeStoreGiveCallback();
        }

        public static bool Buy(string itemId)
        {
            if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
            {
                return ((Currency)StoreInventory[itemId]).Buy();
            }
            else return false;
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
            //{
            //    return ((Consumable)StoreInventory[itemId]).Buy();
            //}
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
            //{
            //    return ((Equipable)StoreInventory[itemId]).Buy();
            //}
            //else
            //{
            //    //UPGRADABLE
            //    return ((Upgradable)StoreInventory[itemId]).Buy();
            //}
        }

        public static string GetPurchaseCurrency(string itemId)
        {
            if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
            {
                return ((Currency)StoreInventory[itemId]).GetPurchaseCurrency();
            }
            else return "";
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
            //{
            //    return ((Consumable)StoreInventory[itemId]).GetPurchaseCurrency();
            //}
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
            //{
            //    return ((Equipable)StoreInventory[itemId]).GetPurchaseCurrency();
            //}
            //else
            //{
            //    //UPGRADABLE
            //    return ((Upgradable)StoreInventory[itemId]).GetPurchaseCurrency();
            //}
        }

        public static int GetPurchaseValue(string itemId)
        {
            if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
            {
                return ((Currency)StoreInventory[itemId]).GetPurchaseValue();
            }
            else return -1;
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
            //{
            //    return ((Consumable)StoreInventory[itemId]).GetPurchaseValue();
            //}
            //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
            //{
            //    return ((Equipable)StoreInventory[itemId]).GetPurchaseValue();
            //}
            //else
            //{
            //    //UPGRADABLE
            //    return ((Upgradable)StoreInventory[itemId]).GetPurchaseValue();
            //}
        }

        /*	public static void checkUncheckEquippables (string itemId)
            {
                if (((Equipable)StoreInventory [itemId]).equipmentCategory == Equipable.EquipmentCategory.BATS) {
                    foreach (Equipable e in equipedBats) {
                        if (e.itemId == itemId) {
                            e.equipNow ();
                        } else {
                            e.unEquipNow ();
                        }
                    }
                } else if (((Equipable)StoreInventory [itemId]).equipmentCategory == Equipable.EquipmentCategory.GLARES) {
                    foreach (Equipable e in equipedGlares) {
                        if (e.itemId == itemId) {
                            e.equipNow ();
                        } else {
                            e.unEquipNow ();
                        }
                    }
                }
                PlayerDataManager.instance.SaveData();
            }
            */

        public static int GetMaxBalance(string itemId)
        {
            if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
            {
                return ((Currency)StoreInventory[itemId]).MaxBalance;
            }
            else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
            {
                return 999999;
            }
            else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
            {
                return 1;
            }
            else
            {
                //UPGRADABLE
                return 5;
            }
        }


        public delegate void StoreGiveCallback();
        public static event StoreGiveCallback onStoreGiveCallback;
        public static void invokeStoreGiveCallback()
        {
            if (onStoreGiveCallback != null)
            {
                onStoreGiveCallback();
            }
        }
    }

}