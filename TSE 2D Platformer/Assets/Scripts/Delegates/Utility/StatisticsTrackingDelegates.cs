using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Delegates.Utility
{
    public class StatisticsTrackingDelegates : MonoBehaviour
    {
        public delegate void OnEnemyDeathTracking();
        public static OnEnemyDeathTracking onEnemyDeathTracking;
    }
}