using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.UI;
using Actors.Player;
using Controllers.Actors.PlayerNS;
using Controllers.Actors.EnemyNS;
using Delegates.Utility;


namespace Controllers.Utility
{
    [RequireComponent (typeof(PlayerController))]
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        UIController _uiController;

        [SerializeField]
        StatisticsTrackerController _statisticsTrackerController;

        Player _player;

        PlayerController _playerController;
        EnemiesController _enemiesController;

        void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _enemiesController = GetComponent<EnemiesController>();

            _enemiesController.BeginSelf();

            _uiController.BeginSelf();
            _statisticsTrackerController.BeginSelf();

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