using Gimmick;
using System;
using UnityEngine;

namespace Character.OperaterState
{
    [Serializable]
    public class GimmickState : IOperaterState
    {
        private OperaterController _operater;
        [SerializeField]
        private CursorController _cursor;

        public GimmickState(OperaterController operaterController)
        {
            _operater = operaterController;

            GameObject.FindGameObjectWithTag("GimmickCanvas").
                transform.Find("Cursor").TryGetComponent(out _cursor);
        }
        /// <summary>
        /// State開始時に実行される
        /// </summary>
        public void HandleStart()
        {
            _cursor.OnStart(_operater.GimmickController.GimmickID);
            _operater.GimmickController.OnStart();
        }
        /// <summary>
        /// フレーム単位で実行される、新しい状態に移行するための条件も書く
        /// </summary>
        public void HandleUpdate()
        {
            _cursor.OnUpdate();
            _operater.GimmickController.OnUpdate();

            // Transition(ICharacterState nextState)を使い移行条件を書く
            if (_cursor.IsClear)
            {
                _operater.StateMachine.Transition(_operater.StateMachine.IdleState);
            }
        }
        /// <summary>
        /// State終了時に実行される
        /// </summary>
        public void HandleEnd()
        {
            _operater.GimmickController.OnEnd();
        }

        
    }
}
