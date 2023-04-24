using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Utility.Statistics;

using Delegates.Utility;


namespace Controllers.Utility
{
    [RequireComponent (typeof(TimeStatisticsTrackerController))]
    [RequireComponent (typeof(RoomStatisticsTrackerController))]
    [RequireComponent (typeof(ActionsTrackerController))]
    [RequireComponent (typeof(CombatTrackerController))]
    [RequireComponent (typeof(ItemsTrackerController))]
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

            SetupDelegates();
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
        }
    }
}