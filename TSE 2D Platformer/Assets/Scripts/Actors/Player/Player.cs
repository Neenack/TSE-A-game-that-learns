using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Actors.Player
{
    [RequireComponent(typeof(PlayerStatsComponent))]
    [RequireComponent(typeof(PlayerMovementComponent))]
    [RequireComponent(typeof(PlayerCollisionsComponent))]

    public class Player : MonoBehaviour
    {
        PlayerStatsComponent _statsComponent;
        PlayerMovementComponent _movementComponent;
        PlayerCollisionsComponent _collisionComponent;

        public void BeginSelf()
        {
            _statsComponent = GetComponent<PlayerStatsComponent>();
            _statsComponent.BeginSelf();

            _movementComponent = GetComponent<PlayerMovementComponent>();
            _movementComponent.BeginSelf();

            _collisionComponent = GetComponent<PlayerCollisionsComponent>();
            _collisionComponent.BeginSelf();
        }
    }
}