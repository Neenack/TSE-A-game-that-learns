using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Actors.Player;
using Actors.Player.PlayerStates;

using Delegates.Actors.Player;


namespace Controllers.Actors.Player 
{
    public class PlayerController : MonoBehaviour
    {
        PlayerMovementStateComponent _playerState;

        float _horizontal;

        float _vertical;
        bool _canJump;


        void Awake() 
        {
            enabled = false;
        }

        public void BeginSelf(ref PlayerMovementStateComponent player)
        {
            _playerState = player;

            _canJump = true;

            SetupDelegates();

            enabled = true;
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

        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Jump");
        }


        void FixedUpdate() 
        {
            if(_horizontal != 0 && PlayerMovementDelegates.onPlayerMoveHorizontal != null)
            {
                PlayerMovementDelegates.onPlayerMoveHorizontal(_horizontal);
            }
            else if(PlayerStateDelegates.onPlayerHorizontalStateChanges != null)
            {
                PlayerStateDelegates.onPlayerHorizontalStateChanges(HorizontalState.Stationary);
            }

            if(_vertical > 0 && _canJump && PlayerMovementDelegates.onPlayerJump != null)
            {
                PlayerMovementDelegates.onPlayerJump();
                _canJump = false;
            }
            else if(_playerState.GetVerticalState() == VerticalState.Grounded)
            {
                _canJump = true;
            }
        
        }
    }
}