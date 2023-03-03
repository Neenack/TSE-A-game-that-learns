using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Actors.Player;

using Actors.Player.PlayerStates;



namespace Controllers.Actors
{
    public class ActorController : MonoBehaviour
    {
        PlayerController _playerController;
        PlayerMovementStateComponent _playerMovementState;

        public void BeginSelf()
        {
            Debug.Log(GameObject.Find("Player"));
            _playerMovementState = GameObject.Find("Player").GetComponent<PlayerMovementStateComponent>();

            _playerController = GetComponent<PlayerController>();
            _playerController.BeginSelf(ref _playerMovementState);
        }
    }
}