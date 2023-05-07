using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Controllers.Utility.Statistics;

using Delegates.Utility;


namespace Controllers.Utility
{
    [RequireComponent (typeof(TimeStatisticsTrackerController))]
    [RequireComponent (typeof(RoomStatisticsTrackerController))]
    [RequireComponent (typeof(ActionsTrackerController))]
    [RequireComponent (typeof(CombatTrackerController))]
    [RequireComponent (typeof(ItemsTrackerController))]
    [RequireComponent (typeof(PlayerStateTrackerController))]
    public class StatisticsTrackerController : MonoBehaviour
    {
        bool first;

        [SerializeField]
        TimeStatisticsTrackerController _timeStatisticsController;
        
        [SerializeField]
        RoomStatisticsTrackerController _roomStatisticsController;
        
        [SerializeField]
        ActionsTrackerController _actionsStatisticsController;
        
        [SerializeField]
        CombatTrackerController _combatStatisticsController;
        
        [SerializeField]
        ItemsTrackerController _itemsStatisticsController;

        [SerializeField]
        PlayerStateTrackerController _playerStateTrackerController;

        //If true, write stats to file
        [SerializeField]
        bool _trainingMode;
        StreamWriter _writer;
        string _fileName = Directory.GetCurrentDirectory() + "/training_data.csv";

        void Awake()
        {
            first = true;
        }
        
        public void BeginSelf()
        {
            _timeStatisticsController.BeginSelf();
            _roomStatisticsController.BeginSelf();
            _actionsStatisticsController.BeginSelf();
            _combatStatisticsController.BeginSelf();
            _itemsStatisticsController.BeginSelf();
            _playerStateTrackerController.BeginSelf();

            SetupDelegates();

            //if in training mode, open file
            if (_trainingMode == true)
            {
                _writer = new StreamWriter( _fileName, true);
                _writer.WriteLine("TEST, TEST2");
            }
        }

        //Needed for closing file
        void OnApplicationQuit()
        {
            if (_writer != null)
            {
                _writer.Close();
            }
        }

        void OnDisable()
        {
            RemoveDelegates();
        }


        void SetupDelegates()
        {
            ZoneDelegates.onZoneCompletion += OnZoneCompletion;
        }

        void RemoveDelegates()
        {
            ZoneDelegates.onZoneCompletion -= OnZoneCompletion;
        }


        void OnZoneCompletion()
        {
            if(first)
            {
                first = false;
                return;
            }

            _timeStatisticsController.OnZoneCompletion();
            _roomStatisticsController.OnZoneCompletion();
            _actionsStatisticsController.OnZoneCompletion();
            _combatStatisticsController.OnZoneCompletion();
            _itemsStatisticsController.OnZoneCompletion();
            _playerStateTrackerController.OnZoneCompletion();
        }
    }
}