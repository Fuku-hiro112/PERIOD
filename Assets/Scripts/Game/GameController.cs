using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class GameController
    {
        private CancellationToken _token;
        private GameMode _mode;
        [SerializeField]
        private ProgressController _progress;
        [SerializeField]
        private GameFacade _facade;
        public GameController(GameMode mode , CancellationToken token)
        {
            _mode = mode;
            _token = token;
            OnStart();
        }
        /// <summary>
        /// Startメソッドから呼ばれる予定　ココがMonoつく可能性アリ
        /// </summary>
        public void OnStart()
        {
            _facade = new GameFacade();
            _progress = new ProgressController();
            _facade.IGardenController.ControlGardenAsync(() => _progress.IncreaseProgress(), _mode, _token);
        }

        public void OnUpdate()
        {
            switch (_mode)
            {
                // WAVE
                case GameMode.Survival:
                    //_facade.IGardenController.ControlGardenAsync(()=>_progress.IncreaseProgress(), _token);
                    break;

                // WAVE関係ない
                case GameMode.Cycle: 

                    break;
            }
        }
    }
}

