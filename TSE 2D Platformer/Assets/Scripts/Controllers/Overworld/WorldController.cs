using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.World.Generation;

namespace Controllers.World
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] LevelGenerationController _levelGenerationController;

        public void BeginSelf()
        {
            _levelGenerationController.BeginSelf();    
        }
    }
}