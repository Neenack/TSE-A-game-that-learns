using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.World;
using Controllers.UI;
using Controllers.Actors;
using Controllers.Utility;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        UIController _uiController;
        WorldController _worldController;
        ActorController _actorController;

        private void Start()
        {
          //TimescaleController.SetTimeScale(0f);

          _uiController = new UIController();

          //PauseState(true);
          
          _worldController = GetComponent<WorldController>();
          _worldController.BeginSelf();

          _actorController = GetComponent<ActorController>();
          _actorController.BeginSelf();

          TimescaleController.SetTimeScale(1f);

          //PauseState(false);          
        }

        /*void PauseState(bool pause)
        {
          _uiController.DisplayPauseScreen(pause);
          TimescaleController.SetTimeScale(1 - (int)pause);
        }*/
    }
}