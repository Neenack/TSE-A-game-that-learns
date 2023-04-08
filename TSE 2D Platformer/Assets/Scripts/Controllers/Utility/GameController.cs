using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Actors.Player;
using Controllers.Actors.PlayerNS;
using Delegates.Utility;


namespace Controllers.Utility
{
    [RequireComponent (typeof(PlayerController))]
    public class GameController : MonoBehaviour
    {
        Player _player;

        PlayerController _playerController;

        void Start()
        {
            _playerController = GetComponent<PlayerController>();
            SetupDelegates();
        }

        void OnDisable()
        {
            RemoveDelegates();
        }

        void SetupDelegates()
        {
            GenerationDelegates.onSpawningPlayer += StartPlayer;
            GenerationDelegates.onDestroyingPlayer += StopPlayer;
        }

        void RemoveDelegates()
        {
            GenerationDelegates.onSpawningPlayer -= StartPlayer;
            GenerationDelegates.onDestroyingPlayer -= StopPlayer;
        }

        // Once the player has spawned, begin the playercontroller.
        void StartPlayer()
        {
            _player = FindObjectOfType<Player>();
            _player.BeginSelf();

            _playerController.BeginSelf(_player);
        }

        // On player despawn, remove it and pause the playercontroller
        void StopPlayer()
        {
            Destroy(_player.gameObject);

            _playerController.PauseSelf();
        }
    }
}