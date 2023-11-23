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
        /// State�J�n���Ɏ��s�����
        /// </summary>
        public void HandleStart()
        {
            _cursor.OnStart(_operater.GimmickController.GimmickID);
            _operater.GimmickController.OnStart();
        }
        /// <summary>
        /// �t���[���P�ʂŎ��s�����A�V������ԂɈڍs���邽�߂̏���������
        /// </summary>
        public void HandleUpdate()
        {
            _cursor.OnUpdate();
            _operater.GimmickController.OnUpdate();

            // Transition(ICharacterState nextState)���g���ڍs����������
            if (_cursor.IsClear)
            {
                _operater.StateMachine.Transition(_operater.StateMachine.IdleState);
            }
        }
        /// <summary>
        /// State�I�����Ɏ��s�����
        /// </summary>
        public void HandleEnd()
        {
            _operater.GimmickController.OnEnd();
        }

        
    }
}
