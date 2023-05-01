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

        float _attack, _useItem;
        float _numpad1, _numpad2, _numpad3, _numpad4;

        bool _jumpButtonReleased, _jumpOnCooldown, _attackButtonReleased, _useItemButtonReleased;
        bool _numpad1Released, _numpad2Released, _numpad3Released, _numpad4Released;

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
            _useItemButtonReleased = true;

            _numpad1Released = true;
            _numpad2Released = true;
            _numpad3Released = true;
            _numpad4Released = true;
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
            _useItem = Input.GetAxis("UseItem");
            

            _numpad1 = Input.GetAxis("Numpad1");
            _numpad2 = Input.GetAxis("Numpad2");
            _numpad3 = Input.GetAxis("Numpad3");
            _numpad4 = Input.GetAxis("Numpad4");
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
            // Attacking
            if(_attack > 0 && _attackButtonReleased && PlayerActionsDelegates.onPlayerAttack != null)
            {
                _attackButtonReleased = false;

                PlayerActionsDelegates.onPlayerAttack();
            }

            else if(_attack == 0)
            {
                _attackButtonReleased = true;
            }


            // Using Item
            if(_useItem > 0 && _useItemButtonReleased && PlayerActionsDelegates.onPlayerUseItem != null)
            {
                _useItemButtonReleased = false;

                PlayerActionsDelegates.onPlayerUseItem();
            }

            else if(_useItem == 0)
            {
                _useItemButtonReleased = true;
            }


            // Numpad1
            if(_numpad1 > 0 && _numpad1Released && PlayerActionsDelegates.onPlayerSwitchItem != null)
            {
                _numpad1Released = false;

                PlayerActionsDelegates.onPlayerSwitchItem(1);
            }

            else if(_numpad1 == 0)
            {
                _numpad1Released = true;
            }


            // Numpad1
            if(_numpad2 > 0 && _numpad2Released && PlayerActionsDelegates.onPlayerSwitchItem != null)
            {
                _numpad2Released = false;

                PlayerActionsDelegates.onPlayerSwitchItem(2);
            }

            else if(_numpad2 == 0)
            {
                _numpad2Released = true;
            }


            // Numpad1
            if(_numpad3 > 0 && _numpad3Released && PlayerActionsDelegates.onPlayerSwitchItem != null)
            {
                _numpad3Released = false;

                PlayerActionsDelegates.onPlayerSwitchItem(3);
            }

            else if(_numpad3 == 0)
            {
                _numpad3Released = true;
            }


            // Numpad1
            if(_numpad4 > 0 && _numpad4Released && PlayerActionsDelegates.onPlayerSwitchItem != null)
            {
                _numpad4Released = false;

                PlayerActionsDelegates.onPlayerSwitchItem(4);
            }

            else if(_numpad4 == 0)
            {
                _numpad4Released = true;
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

