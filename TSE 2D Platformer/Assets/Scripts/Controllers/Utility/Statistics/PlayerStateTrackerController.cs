using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Utility;



namespace Controllers.Utility.Statistics
{
    public class PlayerStateTrackerController : MonoBehaviour
    {
        // 0 = most recent, 11 = last
        float[] _idleTime = new float[11] {0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};

        Coroutine _idleTimeCoroutine = null;

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
            StatisticsTrackingDelegates.onIdleTracking += DoIdleTracking;
        }

        void RemoveDelegates()
        {
            StatisticsTrackingDelegates.onIdleTracking -= DoIdleTracking;
        }


        public void OnZoneCompletion()
        {
            if(_idleTime[0] == -1) _idleTime[0] = 0;
            _idleTime = ShiftRightFloat(_idleTime);
        }


        // Get all the stats from the previous (upto) 10 stages
        float GetIdleTimeAverage()
        {
            float avg = 0;

            for(int i = 1; i <= 10; i++)
            {
                if(_idleTime[i] == -1)
                {
                    if(i > 1)
                    {
                        avg /= i - 1;
                    }
                    return avg;
                }

                avg += _idleTime[i];
            }

            avg /= 10;
            return avg;
        }

        // Add 1 every enemy kill
        // Add 2 first time due to -1 being the check amount
        void DoIdleTracking(bool isIdle)
        {
            if(isIdle)
            {
                _idleTimeCoroutine =  StartCoroutine(IdleTimeIncrementor());
            }

            else
            {
                if(_idleTimeCoroutine != null)
                {
                    StopCoroutine(_idleTimeCoroutine);
                }
            }
        }

        IEnumerator IdleTimeIncrementor()
        {
            while(1 == 1)
            {
                _idleTime[0] += Time.deltaTime;
                yield return null;
            }
        }



        public int[] ShiftRight(int[] arr) 
        {
            int[] demo = new int[arr.Length];

            for (int i = 1; i < arr.Length; i++) 
            {
                demo[i] = arr[i - 1];
            }

            demo[0] = 0;

            return demo;
        }

        public float[] ShiftRightFloat(float[] arr) 
        {
            float[] demo = new float[arr.Length];

            for (int i = 1; i < arr.Length; i++) 
            {
                demo[i] = arr[i - 1];
            }

            demo[0] = 0;

            return demo;
        }
    }
}