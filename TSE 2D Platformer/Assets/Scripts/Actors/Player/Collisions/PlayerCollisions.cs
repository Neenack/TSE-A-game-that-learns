using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Actors.Player.Collisions
{
    [RequireComponent(typeof(CircleCollider2D))]

    public class PlayerCollisions : MonoBehaviour
    {
        // This detects the player hitting the ground, rudimentary, but working
        [SerializeField]
        PlayerGroundCollisions _groundCollisionDetector;
    }
}