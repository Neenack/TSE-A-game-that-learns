using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Utility;



namespace Controllers.Utility.Statistics
{
    public class RoomStatisticsTrackerController : MonoBehaviour
    {
        public void BeginSelf()
        {
            SetupDelegates();
        }

        void OnDisable()
        {
            RemoveDelegates();
        }


        void SetupDelegates()
        {

        }

        void RemoveDelegates()
        {

        }


        public void OnZoneCompletion()
        {
            
        }
    }
}