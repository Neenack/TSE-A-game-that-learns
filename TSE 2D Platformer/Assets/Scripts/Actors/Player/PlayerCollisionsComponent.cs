using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Actors.Player;



namespace Actors.Player
{
    public class PlayerCollisionsComponent : MonoBehaviour
    {
        public void BeginSelf()
        {

        }

        private void OnCollisionEnter2D(Collision2D col) 
        {
            if (col.gameObject.tag == "Walkable")
            {

                if (col.gameObject.tag == "Walkable" && PlayerStateDelegates.onPlayerVerticalStateChanged != null)
                {

                    if (col.gameObject.transform.position.y <= gameObject.transform.position.y)
                    {
                        PlayerStateDelegates.onPlayerVerticalStateChanged(PlayerStates.VerticalState.Grounded);
                        return;
                    }
                }
            }

        }
    }
}