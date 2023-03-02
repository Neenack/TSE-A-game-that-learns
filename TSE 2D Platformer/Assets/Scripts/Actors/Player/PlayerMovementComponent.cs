using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.Player.PlayerStates;

using Delegates.Actors.Player;

namespace Actors.Player
{
    [RequireComponent(typeof(PlayerMovementStateComponent))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementComponent : MonoBehaviour
    {
        Rigidbody2D _rigidbody;

        PlayerMovementStateComponent _stateComponent;


        private void Awake() 
        {
            enabled = false;  
        }
        
        public void BeginSelf()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _stateComponent = GetComponent<PlayerMovementStateComponent>();
            _stateComponent.BeginSelf();


            SetupDelegates();

            enabled = true;
        }

        private void OnDisable()
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {
            PlayerMovementDelegates.onPlayerMoveHorizontal += MoveHorizontal;
            PlayerMovementDelegates.onPlayerJump += Jump;
        }

        void RemoveDelegates()
        {
            PlayerMovementDelegates.onPlayerMoveHorizontal -= MoveHorizontal;
            PlayerMovementDelegates.onPlayerJump -= Jump;
        }


        void MoveHorizontal(float movementPower)
        {
            float playerSpeed = GetComponent<PlayerStatsComponent>().GetMovementSpeed();
            _rigidbody.AddForce(new Vector2(movementPower * playerSpeed, 0));

            if(PlayerStateDelegates.onPlayerVerticalStateChanged != null)
            {
                PlayerStateDelegates.onPlayerHorizontalStateChanges(HorizontalState.Walking);
            }
        }


        void Jump()
        {
            float playerJumpPower = GetComponent<PlayerStatsComponent>().GetJumpPower();
            _rigidbody.AddForce(new Vector2(playerJumpPower, 0));

            if(PlayerStateDelegates.onPlayerVerticalStateChanged != null)
            {
                PlayerStateDelegates.onPlayerVerticalStateChanged(VerticalState.Jumping);
            }
        }


        // Slow player on horizontal plane each frame to stop unnecessarily fast speeds & for smooth acceleration / deceleration.
        void FixedUpdate()
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_rigidbody.velocity.x * 0.64f, _rigidbody.velocity.y);
        }
    }
}