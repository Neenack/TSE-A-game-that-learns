using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Delegates.Actors.Player;
using Delegates.Utility;



namespace Controllers.Utility.Statistics
{
    public class ItemsTrackerController : MonoBehaviour
    {
        int[] _ropesUsed = new int[11] {0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};

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
            PlayerActionsDelegates.onPlayerUseRope += IncrementRopesUsed;
        }

        void RemoveDelegates()
        {
            PlayerActionsDelegates.onPlayerUseRope -= IncrementRopesUsed;
        }


        public void OnZoneCompletion()
        {
            if(_ropesUsed[0] == -1) _ropesUsed[0] = 0;
            _ropesUsed = ShiftRight(_ropesUsed);
        }

        public void ClearStats()
        {
            _ropesUsed = new int[11] { 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        }

        void IncrementRopesUsed()
        {
            _ropesUsed[0]++;
        }


        public float GetRopesUsedAverage()
        {
            float avg = 0;

            for(int i = 1; i <= 10; i++)
            {
                if(_ropesUsed[i] == -1)
                {
                    if(i > 1)
                    {
                        avg /= i - 1;
                    }
                    return avg;
                }

                avg += _ropesUsed[i];
            }

            avg /= 10;
            return avg;
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
    }
}