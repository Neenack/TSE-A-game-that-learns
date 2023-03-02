using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Actors.Player
{
    public class PlayerStatsComponent : MonoBehaviour
    {
        float _movementSpeed = 5f;
        float _jumpPower = 5f;


        public void BeginSelf()
        {
            SetupDelegates();
        }

        private void OnDisable()
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {
        }

        void RemoveDelegates()
        {
        }


        public float GetMovementSpeed()
        {
            return _movementSpeed;
        }

        public float GetJumpPower()
        {
            return _jumpPower;
        }
    }
}