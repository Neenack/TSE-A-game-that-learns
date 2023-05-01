using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Delegates.Actors.Player
{
    public class PlayerActionsDelegates : MonoBehaviour
    {
        public delegate void OnPlayerAttack();
        public static OnPlayerAttack onPlayerAttack;

        public delegate void OnPlayerUseItem();
        public static OnPlayerUseItem onPlayerUseItem;

        public delegate void OnPlayerSwitchItem(int itemNum);
        public static OnPlayerSwitchItem onPlayerSwitchItem;
    }
}
