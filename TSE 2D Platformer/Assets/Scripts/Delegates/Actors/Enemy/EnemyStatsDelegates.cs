using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.EnemyNS;


namespace Delegates.Actors.EnemyNS
{
    public class EnemyStatsDelegates : MonoBehaviour
    {
        public delegate void OnEnemyDeath(Enemy e);
        public static OnEnemyDeath onEnemyDeath;
    }
}