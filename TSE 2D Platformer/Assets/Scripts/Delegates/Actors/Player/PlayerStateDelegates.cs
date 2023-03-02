using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Actors.Player.PlayerStates;


namespace Delegates.Actors.Player
{
    public class PlayerStateDelegates : MonoBehaviour
    {
        public delegate void OnPlayerHorizontalStateChanges(HorizontalState state);
        public static OnPlayerHorizontalStateChanges onPlayerHorizontalStateChanges;

        public delegate void OnPlayerVerticalStateChanged(VerticalState state);
        public static OnPlayerVerticalStateChanged onPlayerVerticalStateChanged;
    }
}