using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.EnemyNS;

using Delegates.Utility;
using Delegates.Actors.EnemyNS;



namespace Controllers.Utility.Statistics
{
    public class CombatTrackerController : MonoBehaviour
    {
        // 0 = most recent, 11 = last
        int[] _enemiesKilled = new int[11] {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};

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
            StatisticsTrackingDelegates.onEnemyDeathTracking += IncrementEnemiesKilled;
        }

        void RemoveDelegates()
        {
            StatisticsTrackingDelegates.onEnemyDeathTracking -= IncrementEnemiesKilled;
        }


        public void OnZoneCompletion()
        {
            if(_enemiesKilled[0] == -1) _enemiesKilled[0] = 0;
            _enemiesKilled = ShiftRight(_enemiesKilled);
        }


        // Get all the stats from the previous (upto) 10 stages
        float GetEnemiesKilledAverage()
        {
            float avg = 0;

            for(int i = 1; i <= 10; i++)
            {
                if(_enemiesKilled[i] == -1)
                {
                    Debug.Log("A");
                    if(i > 1)
                    {
                        avg /= i - 1;
                    }
                    return avg;
                }

                avg += _enemiesKilled[i];
            }

            avg /= 10;
            return avg;
        }

        // Add 1 every enemy kill
        // Add 2 first time due to -1 being the check amount
        void IncrementEnemiesKilled()
        {
            if(_enemiesKilled[0] == -1) _enemiesKilled[0]++;
            _enemiesKilled[0]++;

            Debug.Log(GetEnemiesKilledAverage());
        }



        public int[] ShiftRight(int[] arr) 
        {
            int[] demo = new int[arr.Length];

            for (int i = 1; i < arr.Length; i++) 
            {
                demo[i] = arr[i - 1];
            }

            demo[0] = arr[demo.Length - 1];

            return demo;
        }
    }
}