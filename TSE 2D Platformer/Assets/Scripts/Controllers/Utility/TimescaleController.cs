using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Utility
{
    public static class TimescaleController
    {

        public static void SetTimeScale(float tScale)
        {
            Time.timeScale = tScale;
        }
    }
}