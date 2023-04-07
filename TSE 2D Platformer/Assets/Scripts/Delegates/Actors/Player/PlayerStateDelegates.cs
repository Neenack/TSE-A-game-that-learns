using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.Player;


namespace Delegates.Actors.Player
{
    public class PlayerStateDelegates : MonoBehaviour
    {
        public delegate void OnPlayerGroundedStateChange(PlayerGroundedState pState);
        public static OnPlayerGroundedStateChange onPlayerGroundedStateChange;
    }
}
