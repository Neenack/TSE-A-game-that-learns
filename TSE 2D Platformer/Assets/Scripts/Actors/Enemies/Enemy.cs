using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Actors.EnemyNS;



namespace Actors.EnemyNS
{
    [RequireComponent (typeof(EnemyStats))]

    public class Enemy : MonoBehaviour
    {
        public void BeginSelf()
        {
            SetupDelegates();

            Collider2D blockDetector = Physics2D.OverlapCircle(transform.position, 0.01f);

            if (blockDetector != null && EnemySpawningDelegates.onEnemyDespawn != null)
            {
                EnemySpawningDelegates.onEnemyDespawn(this);
            }
        }

        void SetupDelegates()
        {

        }

        void RemoveDelegates()
        {

        }
    }
}