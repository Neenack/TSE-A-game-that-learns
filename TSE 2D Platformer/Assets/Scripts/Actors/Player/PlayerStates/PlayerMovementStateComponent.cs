using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Delegates.Actors.Player;



namespace Actors.Player.PlayerStates
{
    public enum HorizontalState
    {
        Stationary,
        Walking,
        Running
    }

    public enum VerticalState
    {
        Grounded,
        Jumping
    }

    public class PlayerMovementStateComponent : MonoBehaviour
    {
        HorizontalState _horizontalState;
        VerticalState _verticalState;

        public void BeginSelf()
        {
            _horizontalState = HorizontalState.Stationary;
            _verticalState = VerticalState.Grounded;

            SetupDelegates();
        }

        void OnDisable() 
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {
            PlayerStateDelegates.onPlayerVerticalStateChanged += UpdateVerticalState;
            PlayerStateDelegates.onPlayerHorizontalStateChanges += UpdateHorizontalState;
        }

        void RemoveDelegates()
        {
            PlayerStateDelegates.onPlayerVerticalStateChanged -= UpdateVerticalState;
            PlayerStateDelegates.onPlayerHorizontalStateChanges -= UpdateHorizontalState;
        }


        public HorizontalState GetHorizontalState()
        {
            return _horizontalState;
        }

        public VerticalState GetVerticalState()
        {
            return _verticalState;
        }


        void UpdateHorizontalState(HorizontalState newState)
        {
            _horizontalState = newState;
        }

        void UpdateVerticalState(VerticalState newState)
        {
            _verticalState = newState;
        }
    }
}