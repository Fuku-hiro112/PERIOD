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
        
        // Scripts達
        [SerializeField]
        private ProgressController _progressController;
        [SerializeField]
        private GameFacade _facade;

        public GameController(GameMode mode , CancellationToken token)
        {
            _mode = mode;
            _token = token;
            _progressController = new ProgressController();
            _facade = new GameFacade(_progressController.Progres);
            OnStart();
        }
        /// <summary>
        /// Startメソッドから呼ばれる予定　ココがMonoつく可能性アリ
        /// </summary>
        private void OnStart()
        {
            _facade.IGardenController.ControlGardenAsync(() => _progressController.IncreaseProgress(), _mode, _token);
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

