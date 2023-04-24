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

        float _attack;

        bool _jumpButtonReleased, _jumpOnCooldown, _attackButtonReleased;

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
            _attackButtonReleased = true;
        }

        public void PauseSelf()
        {
            enabled = false;
        }


        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Jump");

            _attack = Input.GetAxis("Attack");
        }

        void FixedUpdate()
        {
            /// Handle Player Movement

            // Handle left/ right walking
            if (_horizontal != 0 && PlayerMovementDelegates.onPlayerMoveHorizontal != null)
            {
                PlayerMovementDelegates.onPlayerMoveHorizontal(_horizontal);

                if(_horizontal > 0 && PlayerStateDelegates.onPlayerTurn != null) PlayerStateDelegates.onPlayerTurn(PlayerFacingDirectionState.Right);
                else if(PlayerStateDelegates.onPlayerTurn != null) PlayerStateDelegates.onPlayerTurn(PlayerFacingDirectionState.Left);
            }

            // Stop player
            else if (_horizontal == 0) PlayerMovementDelegates.onPlayerMoveHorizontal(_horizontal);

            // Handle jumping
            if (_vertical > 0 && !_jumpOnCooldown && _jumpButtonReleased && _playerState.GetGroundedState() == PlayerGroundedState.Grounded && _playerState.GetLadderTouchingState() != PlayerTouchingLadderState.Touching && PlayerMovementDelegates.onPlayerJump != null)
            {
                _jumpButtonReleased = false;
                StartCoroutine(JumpTimer());
                PlayerMovementDelegates.onPlayerJump();
            }

            // Handle ladder movement
            else if(_vertical != 0 && _playerState.GetLadderTouchingState() == PlayerTouchingLadderState.Touching && PlayerMovementDelegates.onPlayerClimb != null)
            {
                if(_vertical > 0) PlayerMovementDelegates.onPlayerClimb(true);
                else PlayerMovementDelegates.onPlayerClimb(false);
            }

            // Jump button has been released
            else if(_vertical == 0) _jumpButtonReleased = true;



            /// Handle Player Actions
            if(_attack > 0 && _attackButtonReleased && PlayerActionsDelegates.onPlayerAttack != null)
            {
                _attackButtonReleased = false;

                PlayerActionsDelegates.onPlayerAttack();
            }

            else if(_attack == 0)
            {
                _attackButtonReleased = true;
            }
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

