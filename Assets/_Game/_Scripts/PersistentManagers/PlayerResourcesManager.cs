using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public enum ResourceID
    {
        //Currency
        KEDOS_ID,

        //Equippable i.e Colors
        FIRST_COLOR_ID,
        SECOND_COLOR_ID,
        THIRD_COLOR_ID
    }
    public enum CurrencyType
    {
        KEDOS
    }
    [System.Serializable]
    public class PlayerResourcesManager : MonoBehaviour
    {
        public static PlayerResourcesManager Instance;
        public static bool debugMode = true;

        bool isStoreManagerInitialized = false;

        public static CurrencyType currencyType = CurrencyType.KEDOS;
        public static ResourceID KEDOS_ITEM_ID = ResourceID.KEDOS_ID;


        [SerializeField]
        public static Dictionary<ResourceID, VirtualItem> StoreInventory;

        [Header("Currencies")]
        public Currency kedos;

        [Header("Equippables")]
        public Equipable firstColor;
        public Equipable secondColor;
        public Equipable thirdColor;

        private void Awake()
        {
            // if the singleton hasn't been initialized yet
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            Instance = this;
        }

        void Start()
        {
            InitializeStoreItems();
        }


        private void Update()
        {
            //if (Input.GetKeyUp(KeyCode.M))
            //{
            //    SovereignUtils.Log($"firstColor balance: {GetBalance(ResourceID.FIRST_COLOR_ID)}");
            //    Buy(ResourceID.FIRST_COLOR_ID);
            //    SovereignUtils.Log($"After Purchase firstColor balance: {GetBalance(ResourceID.FIRST_COLOR_ID)} ");
            //}
        }


        /// <summary>
        /// Initializes the store items.
        /// Call this Method the very first time the store is used
        /// </summary>
        public void InitializeStoreItems()
        {
            if (StoreInventory == null)
            {
                StoreInventory = new Dictionary<ResourceID, VirtualItem>();
            }
            isStoreManagerInitialized = true;

            //Currency define - Start everything with zero balance..aditional items can be given later
            kedos = new Currency(ResourceID.KEDOS_ID);
            firstColor = new Equipable(ResourceID.FIRST_COLOR_ID, false, CurrencyType.KEDOS, 0);
            secondColor = new Equipable(ResourceID.SECOND_COLOR_ID, false, CurrencyType.KEDOS, 0);
            thirdColor = new Equipable(ResourceID.THIRD_COLOR_ID, false, CurrencyType.KEDOS, 0);

            //Add the item to the inventory.
            addItemToStoreInventory(kedos);
            addItemToStoreInventory(firstColor);
            addItemToStoreInventory(secondColor);
            addItemToStoreInventory(thirdColor);
        }

        void addItemToStoreInventory(VirtualItem item)
        {
            if (!StoreInventory.ContainsKey(item.itemId))
                StoreInventory.Add(item.itemId, item);
        }

        public void LoadDataForInspector()
        {
            //Currency
            kedos = (Currency)StoreInventory[ResourceID.KEDOS_ID];
        }

        /// <summary>
        /// This method return available balance for the given ItemID:<br></br>
        /// Currency,<br></br>
        /// Consumable, <br></br>
        /// Equipable<br></br>
        /// </summary>
        /// <returns>The item balance.</returns>
        public static int GetBalance(ResourceID itemId)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {

                case VirtualItemType.CURRENCY:
                    return ((Currency)StoreInventory[itemId]).GetBalance();
                case VirtualItemType.EQUIPABLE:
                    return ((Equipable)StoreInventory[itemId]).GetBalance();
                default:
                    SovereignUtils.Log($"Invalid Item ID {itemId}");
                    return -1;
            }
        }
        /// <summary>
        /// This method returns the available balance for the given currency type.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetBalance(CurrencyType currency)
        {
            switch (currency)
            {
                case CurrencyType.KEDOS:
                    return ((Currency)StoreInventory[ResourceID.KEDOS_ID]).GetBalance();
                default:
                    SovereignUtils.Log($"Invalid Currency type {currency}");
                    return 0;
            }
        }

        public static int GetCollectedBalance(ResourceID itemId)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {

                case VirtualItemType.CURRENCY:
                    return ((Currency)StoreInventory[itemId]).GetCollectedBalance();
                default:
                    Debug.Log("Invalid Item ID");
                    return -1;
            }
        }

        public static int GetNonCollectedBalance(ResourceID itemId)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {

                case VirtualItemType.CURRENCY:
                    return ((Currency)StoreInventory[itemId]).GetNonCollectedBalance();
                default:
                    Debug.Log("Invalid Item ID");

                    return -1;
            }
        }

        /// <summary>
        /// This method gives the coin balance .
        /// </summary>
        /// <returns>Coin Balance</returns>
        public int GetKedosBalance()
        {
            return GetBalance(ResourceID.KEDOS_ID);
        }


        /// <summary>
        /// This method gives the build currency balance .
        /// </summary>
        /// <returns>Build Currency Balance</returns>

        //public int GetBuildCurrencyBalance() => GetBalance(BUILD_CURRENCY_ITEM_ID);




        /// <summary>
        /// This Method Adds the given amount of coins .
        /// </summary>
        /// <param name="quantity"></param>
        public void AddKedos(int quantity)
        {
            Give(ResourceID.KEDOS_ID, quantity);
            PlayerDataManager.instance.SaveData();
        }

        /// <summary>
        /// This method removes the given amount of coins.
        /// If the  balance is less than 0 it will resets to 0.
        /// </summary>
        /// <param name="quantity"></param>
        public void DeductKedos(int quantity)
        {
            Take(ResourceID.KEDOS_ID, quantity);
            PlayerDataManager.instance.SaveData();
        }

        public static void ResetBalance(ResourceID itemId)
        {
            if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
            {
                ((Currency)StoreInventory[itemId]).ResetBalance();
            }
            PlayerDataManager.instance.SaveData();
        }
        public static void Take(ResourceID itemId)
        {
            Take(itemId, 1);
        }

        public static void Take(ResourceID itemId, int quantity)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {
                case VirtualItemType.CURRENCY:
                    ((Currency)StoreInventory[itemId]).Take(quantity);
                    break;
                default:
                    SovereignUtils.LogError($"Invalid ItemID from Take: {itemId} PlayerResourcesManager");
                    break;
            }
            if (debugMode)
            {
                Debug.Log("Take - " + itemId + " - " + quantity + "\nCurrent Balance - " + GetBalance(itemId));
            }
            PlayerDataManager.instance.SaveData();
            invokeStoreGiveCallback();
        }

        public static void Give(ResourceID itemId)
        {
            Give(itemId, 1);
        }

        public static void Give(ResourceID itemId, int quantity)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {
                case VirtualItemType.CURRENCY:
                    ((Currency)StoreInventory[itemId]).Give(quantity);
                    break;
                default:
                    SovereignUtils.LogError($"Invalid Item ID: {itemId} PlayerResourcesManager");
                    break;
            }
            if (debugMode)
            {
                Debug.Log("BEfore Give - " + itemId + " - " + quantity + "\nCurrent Balance - " + GetBalance(itemId));
            }
            PlayerDataManager.instance.SaveData();
            invokeStoreGiveCallback();
        }

        public static void UpdateToPlayerData(ResourceID itemId)
        {

            switch (StoreInventory[itemId].virtualItemType)
            {
                case VirtualItemType.CURRENCY:
                    ((Currency)StoreInventory[itemId]).UpdateBalance();
                    break;
                default:
                    SovereignUtils.LogError($"Invalid Item ID: {itemId} PlayerResourcesManager");
                    break;
            }

            PlayerDataManager.instance.SaveData();
            invokeStoreGiveCallback();
        }

        public static bool Buy(ResourceID itemId)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {
                case VirtualItemType.CURRENCY:
                    return ((Currency)StoreInventory[itemId]).Buy();
                case VirtualItemType.EQUIPABLE:
                    return ((Equipable)StoreInventory[itemId]).Buy();
                default:
                    SovereignUtils.LogError($"Invalid Item ID: {itemId} PlayerResourcesManager");
                    return false;
            }
        }

        //public static string GetPurchaseCurrency(ResourceID itemId)
        //{
        //    if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
        //    {
        //        return ((Currency)StoreInventory[itemId]).GetPurchaseCurrency();
        //    }
        //    else return "";
        //    //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
        //    //{
        //    //    return ((Consumable)StoreInventory[itemId]).GetPurchaseCurrency();
        //    //}
        //    //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
        //    //{
        //    //    return ((Equipable)StoreInventory[itemId]).GetPurchaseCurrency();
        //    //}
        //    //else
        //    //{
        //    //    //UPGRADABLE
        //    //    return ((Upgradable)StoreInventory[itemId]).GetPurchaseCurrency();
        //    //}
        //}

        //public static int GetPurchaseValue(ResourceID itemId)
        //{
        //    if (StoreInventory[itemId].virtualItemType == VirtualItemType.CURRENCY)
        //    {
        //        return ((Currency)StoreInventory[itemId]).GetPurchaseValue();
        //    }
        //    else return -1;
        //    //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.CONSUMABLE)
        //    //{
        //    //    return ((Consumable)StoreInventory[itemId]).GetPurchaseValue();
        //    //}
        //    //else if (StoreInventory[itemId].virtualItemType == VirtualItemType.EQUIPABLE)
        //    //{
        //    //    return ((Equipable)StoreInventory[itemId]).GetPurchaseValue();
        //    //}
        //    //else
        //    //{
        //    //    //UPGRADABLE
        //    //    return ((Upgradable)StoreInventory[itemId]).GetPurchaseValue();
        //    //}
        //}

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

        public static int GetMaxBalance(ResourceID itemId)
        {
            switch (StoreInventory[itemId].virtualItemType)
            {
                case VirtualItemType.CURRENCY:
                    return ((Currency)StoreInventory[itemId]).MaxBalance;
                case VirtualItemType.CONSUMABLE:
                    return 999999;
                case VirtualItemType.EQUIPABLE:
                    return 1;
                case VirtualItemType.UPGRADABLE:
                    return 5;
                default:
                    SovereignUtils.LogError($"Invalid Item ID: {itemId} PlayerResourcesManager");
                    return 0;

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