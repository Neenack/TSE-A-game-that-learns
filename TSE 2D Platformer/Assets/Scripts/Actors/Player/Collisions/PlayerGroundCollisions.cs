using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Actors.Player;

namespace Actors.Player.Collisions
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerGroundCollisions : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Walkable" && PlayerStateDelegates.onPlayerGroundedStateChange != null)
            {
                PlayerStateDelegates.onPlayerGroundedStateChange(PlayerGroundedState.Grounded);
                return;
            }   
        }

        void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.tag == "Walkable" && PlayerStateDelegates.onPlayerGroundedStateChange != null)
            {
                PlayerStateDelegates.onPlayerGroundedStateChange(PlayerGroundedState.Aerial);
                return;
            }
        }
    }
}