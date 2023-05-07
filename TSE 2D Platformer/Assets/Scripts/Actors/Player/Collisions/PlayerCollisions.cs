using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Actors.Player;


namespace Actors.Player.Collisions
{
    [RequireComponent(typeof(CircleCollider2D))]

    public class PlayerCollisions : MonoBehaviour
    {
        // This detects the player hitting the ground, rudimentary, but working
        [SerializeField]
        PlayerGroundCollisions _groundCollisionDetector;


        void OnTriggerStay2D(Collider2D col)
        {
            if (col.tag == "PlayerChildObjectTag") return;


            if (col.gameObject.tag == "Climbable" && PlayerStateDelegates.onPlayerLadderTouchingStateChange != null)
            {

                PlayerStateDelegates.onPlayerLadderTouchingStateChange(PlayerTouchingLadderState.Touching);
                return;
            }

            //Handles player death
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyProjectile" || col.gameObject.tag == "Trap")
            {
                PlayerStateDelegates.onPlayerDeathStateChange(PlayerDeathState.Dead);
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag == "PlayerChildObjectTag") return;

            if (col.gameObject.tag == "Climbable" && PlayerStateDelegates.onPlayerLadderTouchingStateChange != null)
            {

                PlayerStateDelegates.onPlayerLadderTouchingStateChange(PlayerTouchingLadderState.Not_Touching);
                return;
            }

        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            //Handles player death
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyProjectile" || col.gameObject.tag == "Trap")
            {
                PlayerStateDelegates.onPlayerDeathStateChange(PlayerDeathState.Dead);
            }
        }
    }
}