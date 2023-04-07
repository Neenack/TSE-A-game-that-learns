using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Actors.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        float _speed, _jumpPower;


        public float GetSpeed()
        {
            return _speed;
        }

        public float GetJumpPower()
        {
            return _jumpPower;
        }
        

        void SetSpeed(float val)
        {
            _speed = val;
        }

        void SetJumpPower(float val)
        {
            _jumpPower = val;
        }
    }
}