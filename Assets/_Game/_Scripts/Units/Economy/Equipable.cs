using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{

    [System.Serializable]
    public class Equipable : VirtualItem
    {
        public bool isPurchased;
        public bool isEquiped;
        public int purchasePrice;
        public CurrencyType purchaseCurrency;
        //  public EquipmentCategory equipmentCategory;

        public enum EquipmentCategory
        {
            FIRST_COLOR,
            SECOND_COLOR,
            THIRD_COLOR
        }

        public Equipable(ResourceID itemId, bool isPurchased, CurrencyType purchaseCurrency, int purchasePrice /*,EquipmentCategory equipmentCategory*/)
            : base(itemId, VirtualItemType.EQUIPABLE)
        {
            this.isPurchased = isPurchased;
            this.purchaseCurrency = purchaseCurrency;
            this.purchasePrice = purchasePrice;
            //  this.equipmentCategory = equipmentCategory;
        }
        public override int GetBalance()
        {
            if (IsPurchased)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override bool Buy()
        {
            if (IsPurchased)
            {
                equipNow();
                return true;
            }
            else
            {
                if (CanAfford())
                {
                    //StoreInventory.TakeItem(SoomlaStoreDefinition1.COIN_CURRENCY_ITEM_ID,getUpgradeValue());
                    DeductCurrencyBalance(purchaseCurrency, purchasePrice);
                    IsPurchased = true;
                    equipNow();
                    return true;
                }
                else
                {
                    Debug.LogWarning("Not enough cash!!");
                    return false;
                }
            }
        }

        public override void Give(int quantity)
        {
            //quantity is ignored
            IsPurchased = true;
        }

        public override void Take(int quantity)
        {//Quantity is ignored
            IsPurchased = false;
        }

        public bool IsPurchased
        {
            get
            {
                return this.isPurchased;
            }
            set
            {
                isPurchased = value;
            }
        }

        public void equipNow()
        {
            ///this should trigger a far each loop that will deactivate other equipables
            isEquiped = true;
        }

        public void unEquipNow()
        {
            isEquiped = false;
        }


        /// <summary>
        /// Return true if user can afford to buy the product.
        /// Color text GREEN if returns true
        /// Color text RED if returns false
        /// </summary>
        public override bool CanAfford()
        {
            //Adding a plus 1 to current level as we want to get that level and we start of at level 0
            if (PlayerResourcesManager.GetBalance(purchaseCurrency) >= GetPurchaseValue())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override CurrencyType GetPurchaseCurrency()
        {
            return purchaseCurrency;
        }

        public override int GetPurchaseValue()
        {
            return purchasePrice;
        }
    }
}