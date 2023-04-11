using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Delegates.Actors.Player;


namespace Actors.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        // Stop FixedUpdate firing too early
        void Awake()
        {
            enabled = false;
        }

        Rigidbody2D _rigidBody;

        public void BeginSelf()
        {
            enabled = true;

            _rigidBody = GetComponent<Rigidbody2D>();

            SetupDelegates();
        }

        void OnDisable()
        {
            RemoveDelegates();
        }

        
        void SetupDelegates()
        {
            PlayerMovementDelegates.onPlayerMoveHorizontal += MoveHorizontal;
            PlayerMovementDelegates.onPlayerJump += Jump;
            PlayerMovementDelegates.onPlayerClimb += Climb;

            PlayerStateDelegates.onPlayerLadderTouchingStateChange += UpdatePlayerGravityOnLadderStateChange;
        }

        void RemoveDelegates()
        {
            PlayerMovementDelegates.onPlayerMoveHorizontal -= MoveHorizontal;
            PlayerMovementDelegates.onPlayerJump -= Jump;
            PlayerMovementDelegates.onPlayerClimb -= Climb;

            PlayerStateDelegates.onPlayerLadderTouchingStateChange -= UpdatePlayerGravityOnLadderStateChange;
        }


        void MoveHorizontal(float horizontalSpeed)
        {
            _rigidBody.velocity = new Vector2(horizontalSpeed * GetComponent<PlayerStats>().GetSpeed(), _rigidBody.velocity.y);
        }

        void Jump()
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, GetComponent<PlayerStats>().GetJumpPower());
        }


        void UpdatePlayerGravityOnLadderStateChange(PlayerTouchingLadderState pState)
        {
            if(pState == PlayerTouchingLadderState.Not_Touching)
            {
                _rigidBody.gravityScale = 6;
            }

            else
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
                _rigidBody.gravityScale = 0;
            }
        }

        void Climb(bool up)
        {
            if(up)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, GetComponent<PlayerStats>().GetClimbSpeed());
            }

            else
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, GetComponent<PlayerStats>().GetClimbSpeed() * -1);
            }
        }


        /* Slow player on horizontal plane each frame to stop sliding and fast speeds
        void FixedUpdate()
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x * 0.64f, _rigidBody.velocity.y);
        }*/
    }
}