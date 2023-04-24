using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Delegates.Actors.Player
{
    public class PlayerActionsDelegates : MonoBehaviour
    {
        public delegate void OnPlayerAttack();
        public static OnPlayerAttack onPlayerAttack;
    }
}
