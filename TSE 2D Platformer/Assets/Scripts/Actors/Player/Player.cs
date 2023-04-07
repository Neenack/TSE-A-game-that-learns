using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.Player.Collisions;



namespace Actors.Player
{
    [RequireComponent (typeof(PlayerStats))]
    [RequireComponent (typeof(PlayerMovement))]
    [RequireComponent (typeof(PlayerState))]
    [RequireComponent (typeof(PlayerCollisions))]

    public class Player : MonoBehaviour
    {
        [SerializeField]
        PlayerStats _statsComponent;

        [SerializeField]
        PlayerMovement _movementComponent;

        [SerializeField]
        PlayerState _stateComponent;

        [SerializeField]
        PlayerCollisions _collisionsComponent;


        // Setup scripts that need to begin
        public void BeginSelf()
        {
            _movementComponent.BeginSelf();
            _stateComponent.BeginSelf();
        }
    }
}