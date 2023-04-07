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
        }

        void RemoveDelegates()
        {
            PlayerMovementDelegates.onPlayerMoveHorizontal -= MoveHorizontal;
            PlayerMovementDelegates.onPlayerJump -= Jump;
        }


        void MoveHorizontal(float horizontalSpeed)
        {
            _rigidBody.AddForce(new Vector2(horizontalSpeed * GetComponent<PlayerStats>().GetSpeed(), 0));
        }

        void Jump()
        {
            _rigidBody.AddForce(new Vector2(0, GetComponent<PlayerStats>().GetJumpPower()));
        }


        // Slow player on horizontal plane each frame to stop sliding and fast speeds
        void FixedUpdate()
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x * 0.64f, _rigidBody.velocity.y);
        }
    }
}