using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Actors.EnemyNS;



namespace Actors.EnemyNS
{
    public class Enemy : MonoBehaviour
    {
        public void BeginSelf()
        {
            SetupDelegates();

            List<Collider2D> collisions = new List<Collider2D>();

            RaycastHit2D[] blockDetector = Physics2D.CircleCastAll(transform.position, 0.01f, new Vector2(0, 0), 0f, 0);

            if (blockDetector != null && EnemySpawningDelegates.onEnemyDespawn != null)
            {
                foreach (RaycastHit2D col in blockDetector)
                {
                    if (col.transform.tag == "Walkable")
                    {
                        EnemySpawningDelegates.onEnemyDespawn(this);
                    }
                }
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