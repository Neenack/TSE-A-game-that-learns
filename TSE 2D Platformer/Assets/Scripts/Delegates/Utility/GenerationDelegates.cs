using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Delegates.Utility
{
    public class GenerationDelegates : MonoBehaviour
    {
        public delegate void OnSpawningPlayer();
        public static OnSpawningPlayer onSpawningPlayer;
    }
}