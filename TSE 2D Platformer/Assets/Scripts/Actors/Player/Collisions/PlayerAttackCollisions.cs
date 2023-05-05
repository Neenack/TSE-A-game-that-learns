using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.EnemyNS;
using Delegates.Actors.EnemyNS;



namespace Actors.Player.Collisions
{
    public class PlayerAttackCollisions : MonoBehaviour
    {
        List<Enemy> enemiesInRange;

        public void BeginSelf()
        {
            enemiesInRange = new List<Enemy>();

            SetupDelegates();
        }

        void OnDisable()
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {
            EnemyStatsDelegates.onEnemyDeath += RemoveFromInRange;
        }

        void RemoveDelegates()
        {
            EnemyStatsDelegates.onEnemyDeath -= RemoveFromInRange;
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "Player") return;

            if (col.GetComponent<Enemy>() != null)
            {
                enemiesInRange.Add(col.GetComponent<Enemy>());
                return;
            }   
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if(col.tag == "Player") return;

            if (col.GetComponent<Enemy>() != null && enemiesInRange.Contains(col.GetComponent<Enemy>()))
            {
                enemiesInRange.Remove(col.GetComponent<Enemy>());
                return;
            }
        }


        public List<Enemy> GetEnemiesList()
        {
            return enemiesInRange;
        }


        void RemoveFromInRange(Enemy e)
        {
            if(enemiesInRange.Contains(e))
            {
                enemiesInRange.Remove(e);
            }
        }


        public void ClearInRange()
        {
            enemiesInRange.Clear();
        }
    }
}