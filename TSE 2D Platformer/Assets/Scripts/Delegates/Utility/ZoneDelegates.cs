using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Delegates.Utility
{
    public class ZoneDelegates : MonoBehaviour
    {
        public delegate void OnZoneCompletion();
        public static OnZoneCompletion onZoneCompletion;

        public delegate void OnZoneGenerationStart();
        public static OnZoneGenerationStart onZoneGenerationStart;

        public delegate void OnZoneTickUpdate();
        public static OnZoneTickUpdate onZoneTickUpdate;

        public delegate void OnZoneGenerationFinish();
        public static OnZoneGenerationFinish onZoneGenerationFinish;
    }
}