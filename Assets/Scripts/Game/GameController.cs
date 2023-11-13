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
        
        // Scripts�B
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
        /// Start���\�b�h����Ă΂��\��@�R�R��Mono���\���A��
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

                // WAVE�֌W�Ȃ�
                case GameMode.Cycle: 

                    break;
            }
        }
    }
}

