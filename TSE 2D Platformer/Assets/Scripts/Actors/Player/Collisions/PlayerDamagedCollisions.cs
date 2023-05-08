using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Actors.EnemyNS;

using Delegates.Utility;


namespace Actors.Player.Collisions
{
    public class PlayerDamagedCollisions : MonoBehaviour
    {
        bool _isDead;


        void Start()
        {
            _isDead = false;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.tag == "Player") return;

            if(col.gameObject.tag == "Enemy")
            {
                if(StatisticsTrackingDelegates.onPlayerHitByEnemy != null) StatisticsTrackingDelegates.onPlayerHitByEnemy(col.gameObject.GetComponent<EnemyStats>().GetType());
                _isDead = true;
                return;
            }

            if(col.gameObject.tag == "Projectile")
            {
                if(StatisticsTrackingDelegates.onPlayerHitByEnemy != null) StatisticsTrackingDelegates.onPlayerHitByEnemy(EnemyType.Screamer);
                _isDead = true;
                return;
            }

            if(col.gameObject.tag == "Trap")
            {
                if(StatisticsTrackingDelegates.onPlayerHitByTrap != null) StatisticsTrackingDelegates.onPlayerHitByTrap();
                _isDead = true;
                return;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if(col.tag == "Player") return;
        }
    }
}