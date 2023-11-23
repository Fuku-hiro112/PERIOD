using Gimmick;
using System;
using UnityEngine;

namespace Character.OperaterState
{
    [Serializable]
    public class GimmickState : IOperatorState
    {
        private OperatorController _operator;
        [SerializeField]
        private CursorController _cursor;

        public GimmickState(OperatorController operatorController)
        {
            _operator = operatorController;

            GameObject.FindGameObjectWithTag("GimmickCanvas").
                transform.Find("Cursor").TryGetComponent(out _cursor);
        }
        /// <summary>
        /// State開始時に実行される
        /// </summary>
        public void HandleStart()
        {
            _operator.IsAction = true;
            _cursor.OnStart(_operator.GimmickController.GimmickID);
            _operator.GimmickController.OnStart();
        }
        /// <summary>
        /// フレーム単位で実行される、新しい状態に移行するための条件も書く
        /// </summary>
        public void HandleUpdate()
        {
            _cursor.OnUpdate();
            _operator.GimmickController.OnUpdate();

            // Transition(ICharacterState nextState)を使い移行条件を書く
            if (_cursor.IsClear)
            {
                _operator.StateMachine.Transition(_operator.StateMachine.IdleState);
            }
        }
        /// <summary>
        /// State終了時に実行される
        /// </summary>
        public void HandleEnd()
        {
            _operator.IsAction = false;
            _operator.GimmickController.OnEnd();
        }

        
    }
}
