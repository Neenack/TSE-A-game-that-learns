using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Utility.Statistics;



namespace Delegates.Utility
{
    public class StatisticsTrackingDelegates : MonoBehaviour
    {
        public delegate void OnEnemyDeathTracking();
        public static OnEnemyDeathTracking onEnemyDeathTracking;

        public delegate void OnRoomExplored();
        public static OnRoomExplored onRoomExplored;

        public delegate void OnIdleTracking(bool isIdle);
        public static OnIdleTracking onIdleTracking;

        public delegate void OnActionTracking(ActionType aType);
        public static OnActionTracking onActionTracking;
    }
}