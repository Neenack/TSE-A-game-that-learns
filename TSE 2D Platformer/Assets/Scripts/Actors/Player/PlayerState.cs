using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Actors.Player
{
    [RequireComponent (typeof(Rigidbody2D))]

    public class PlayerState : MonoBehaviour
    {
        // PlayerMovementState currently does nothing, to be implemented more fully at a later point
        PlayerMovementState _movementState;
        PlayerGroundedState _groundedState;

        public void BeginSelf()
        {
            _movementState = PlayerMovementState.Idle;
            _groundedState = PlayerGroundedState.Grounded;
        }


        public PlayerMovementState GetMovementState()
        {
            return _movementState;
        }

        public PlayerGroundedState GetGroundedState()
        {
            return _groundedState;
        }
    }


    public enum PlayerMovementState
    {
        Idle,
        In_Motion
    }

    public enum PlayerGroundedState
    {
        Grounded,
        Aerial
    }
}