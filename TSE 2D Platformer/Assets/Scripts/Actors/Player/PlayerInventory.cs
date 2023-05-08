using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.Player.Items;

using Controllers.Utility.Statistics;

using Delegates.Actors.Player;
using Delegates.Utility;



namespace Actors.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        Item[] _inventory = new Item[4] {new BombItem(), new RopeItem(), null, null}; 
        int[] _amounts = new int[4] {5, 5, 0, 0};

        int _selectedItem;

        public void BeginSelf()
        {
            _selectedItem = 1;
            SetupDelegates();

            if(GenerationDelegates.onInventoryInitialized != null)
            {
                GenerationDelegates.onInventoryInitialized();
            }
        }

        void OnDisable()
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {
            PlayerActionsDelegates.onPlayerSwitchItem += SwitchActiveItem;
            PlayerActionsDelegates.onPlayerUseItem += UseItem;
        }

        void RemoveDelegates()
        {
            PlayerActionsDelegates.onPlayerSwitchItem -= SwitchActiveItem;
            PlayerActionsDelegates.onPlayerUseItem -= UseItem;
        }


        public int GetSelectedItem()
        {
            return _selectedItem;
        }

        public int GetAmount(int itemNum)
        {
            return _amounts[itemNum];
        }



        void SwitchActiveItem(int itemNum)
        {
            _selectedItem = itemNum;

            Debug.Log("Switched to " + _selectedItem);
        }

        void UseItem()
        {
            if(_amounts[_selectedItem - 1] > 0)
            {
                _inventory[_selectedItem - 1].UseItem();
                _amounts[_selectedItem - 1]--;

                if(StatisticsTrackingDelegates.onActionTracking != null) StatisticsTrackingDelegates.onActionTracking(ActionType.Item);
            }
        }
    }
}