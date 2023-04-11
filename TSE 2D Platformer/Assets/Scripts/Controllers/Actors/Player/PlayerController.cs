using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.Player;
using Delegates.Actors.Player;


namespace Controllers.Actors.PlayerNS
{
    public class PlayerController : MonoBehaviour
    {
        PlayerState _playerState;

        float _horizontal, _vertical;

        bool _jumpButtonReleased, _jumpOnCooldown;

        // Stop Update & Fixed Update from taking place until spawn
        void Awake()
        {
            enabled = false;
        }
        
        // Called from GameController on spawn, enables Update & FixedUpdate.
        public void BeginSelf(Player player)
        {
            _playerState = player.GetComponent<PlayerState>();

            enabled = true;

            _jumpButtonReleased = true;
        }

        public void PauseSelf()
        {
            enabled = false;
        }


        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Jump");
        }

        void FixedUpdate()
        {
            if (_horizontal != 0 && PlayerMovementDelegates.onPlayerMoveHorizontal != null)
            {
                PlayerMovementDelegates.onPlayerMoveHorizontal(_horizontal);
            }

            else if (_horizontal == 0) PlayerMovementDelegates.onPlayerMoveHorizontal(_horizontal);

            if (_vertical > 0 && !_jumpOnCooldown && _jumpButtonReleased && _playerState.GetGroundedState() == PlayerGroundedState.Grounded && _playerState.GetLadderTouchingState() != PlayerTouchingLadderState.Touching && PlayerMovementDelegates.onPlayerJump != null)
            {
                _jumpButtonReleased = false;
                StartCoroutine(JumpTimer());
                PlayerMovementDelegates.onPlayerJump();
            }

            else if(_vertical != 0 && _playerState.GetLadderTouchingState() == PlayerTouchingLadderState.Touching && PlayerMovementDelegates.onPlayerClimb != null)
            {
                if(_vertical > 0) PlayerMovementDelegates.onPlayerClimb(true);
                else PlayerMovementDelegates.onPlayerClimb(false);
            }

            else if(_vertical == 0) _jumpButtonReleased = true;


            // To be added later, allowing the sprite to turn as the player does
            /*if (_horizontal > 0 && PlayerMovementDelegates.onPlayerTurn != null)
            {
                PlayerMovementDelegates.onPlayerTurn(TravellingDirection.Right);
            }

            else if (_horizontal < 0 && PlayerMovementDelegates.onPlayerTurn != null)
            {
                PlayerMovementDelegates.onPlayerTurn(TravellingDirection.Left);
            }*/
        }


        // Fix that stops player from jumping every frame since they haven't entirely left the ground yet.
        IEnumerator JumpTimer()
        {
            _jumpOnCooldown = true;
            yield return new WaitForSeconds(0.05f);
            _jumpOnCooldown = false;
        }
    }
}

