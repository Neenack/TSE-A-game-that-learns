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
        //Needed for writing stats
        StreamWriter _writer;
        string _fileName = Directory.GetCurrentDirectory() + "/training_data.csv";
        GameObject _levelGen;
        LevelGeneration _levelGenScript;
        int _difficulty;
        int _newDifficulty;
        bool _storeStatsOnClosing = false;
        List<float> _dataPoint = new List<float>();
        string _dataPointString;


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
                _levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");
                _levelGenScript = _levelGen.GetComponent<LevelGeneration>();
                _difficulty = _levelGenScript.difficulty;             
            }
        }

        //Needed for closing file
        void OnApplicationQuit()
        {
            if (_writer != null)
            {
                //Writes final stats (if level vompleted without difficulty change)
                if (_storeStatsOnClosing == true)
                {
                    WriteDataPoint();
                }
                
                //Close file
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

            //If difficulty changes, track previous results
            if (_trainingMode == false)
            {
                return;
            }
            _newDifficulty = _levelGenScript.difficulty;
            if (_difficulty != _newDifficulty)
            {
                WriteDataPoint();
                _difficulty = _newDifficulty;
                _storeStatsOnClosing = false;

            }

            //If a level has been completed regardelss of difficulty change, stats should be stored when the game closes
            else
            {
                _storeStatsOnClosing = true;
            }
        }

        void WriteDataPoint()
        {
            //Add features to data point
            _dataPoint.Add(_timeStatisticsController.GetZoneTimeAverage());
            _dataPoint.Add(_roomStatisticsController.GetRoomsExploredAverage());
            _dataPoint.Add(_roomStatisticsController.GetLongestRoomTimeAverage());
            _dataPoint.Add(_actionsStatisticsController.GetActionsPerformedAverage(ActionType.Jump));
            _dataPoint.Add(_actionsStatisticsController.GetActionsPerformedAverage(ActionType.Item));
            _dataPoint.Add(_actionsStatisticsController.GetActionsPerformedAverage(ActionType.Attack));
            _dataPoint.Add(_combatStatisticsController.GetEnemiesKilledAverage());
            _dataPoint.Add(_combatStatisticsController.GetNearMissesWithEnemyAverage());
            _dataPoint.Add(_combatStatisticsController.GetNearMissesWithProjectileAverage());
            _dataPoint.Add(_combatStatisticsController.GetBombKillsAverage());
            _dataPoint.Add(_itemsStatisticsController.GetRopesUsedAverage());
            _dataPoint.Add(_playerStateTrackerController.GetIdleTimeAverage());
            _dataPoint.Add(_playerStateTrackerController.GetEnemiesDetectedAverage());
            _dataPoint.Add(_playerStateTrackerController.GetDeathToAngryBobAverage());
            _dataPoint.Add(_playerStateTrackerController.GetDeathToScreamerAverage());
            _dataPoint.Add(_playerStateTrackerController.GetDeathToJumperAverage());
            _dataPoint.Add(_playerStateTrackerController.GetDeathToTrap());
            //Turn into string with commas to write to csv
            foreach (float feature in _dataPoint)
            {
                Debug.Log(feature);
                _dataPointString += feature + ",";
            }
            //Remove final comma
            _dataPointString = _dataPointString.Substring(0, _dataPointString.Length - 1);
            Debug.Log(_dataPointString);
            _writer.WriteLine(_dataPointString);

            //Reset stats: want fresh stats for each difficulty when collecting training data
            _timeStatisticsController.ClearStats();
            _roomStatisticsController.ClearStats();
            _actionsStatisticsController.ClearStats();
            _combatStatisticsController.ClearStats();
            _itemsStatisticsController.ClearStats();
            _playerStateTrackerController.ClearStats();

            //Reset datapoint vars
            _dataPoint.Clear();
            _dataPointString = string.Empty;
        }
    }
}