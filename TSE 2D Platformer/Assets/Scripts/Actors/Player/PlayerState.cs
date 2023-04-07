using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Actors.Player;



namespace Actors.Player
{
    [RequireComponent (typeof(Rigidbody2D))]

    public class PlayerState : MonoBehaviour
    {
        // PlayerMovementState currently does nothing, to be implemented more fully at a later point
        PlayerMovementState _movementState;
        PlayerGroundedState _groundedState;

        PlayerTouchingLadderState _touchingLadderState;

        public void BeginSelf()
        {
            _movementState = PlayerMovementState.Idle;
            _groundedState = PlayerGroundedState.Grounded;
            _touchingLadderState = PlayerTouchingLadderState.Not_Touching;
            
            SetupDelegates();
        }

        void OnDisable()
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {

            PlayerStateDelegates.onPlayerGroundedStateChange += SetGroundedState;
            PlayerStateDelegates.onPlayerLadderTouchingStateChange += SetLadderTouchingState;
        }

        void RemoveDelegates()
        {

            PlayerStateDelegates.onPlayerGroundedStateChange -= SetGroundedState;
            PlayerStateDelegates.onPlayerLadderTouchingStateChange -= SetLadderTouchingState;
        }


        public PlayerMovementState GetMovementState()
        {
            return _movementState;
        }

        public PlayerGroundedState GetGroundedState()
        {
            return _groundedState;
        }

        public PlayerTouchingLadderState GetLadderTouchingState()
        {
            return _touchingLadderState;
        }


        public void SetMovementState(PlayerMovementState pState)
        {
            _movementState = pState;
        }

        public void SetGroundedState(PlayerGroundedState pState)
        {
            _groundedState = pState;
        }

        public void SetLadderTouchingState(PlayerTouchingLadderState pState)
        {
            _touchingLadderState = pState;
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

    public enum PlayerTouchingLadderState
    {
        Not_Touching,
        Touching
    }
}