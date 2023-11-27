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
        [SerializeField]
        private InputManager _inputManager;
 
        public GimmickState(OperatorController operatorController)
        {
            _operator = operatorController;

            GameObject.FindGameObjectWithTag("GimmickCanvas").
                transform.Find("Cursor").TryGetComponent(out _cursor);

            _inputManager = GameObject.Find("Input").GetComponent<InputManager>();
        }
        /// <summary>
        /// State�J�n���Ɏ��s�����
        /// </summary>
        public void HandleStart()
        {
            _operator.IsAction = true;
            _inputManager.ActionMapChange("UI");
            _cursor.OnStart(_operator.GimmickController.GimmickID);
            _operator.GimmickController.OnStart();
        }
        /// <summary>
        /// �t���[���P�ʂŎ��s�����A�V������ԂɈڍs���邽�߂̏���������
        /// </summary>
        public void HandleUpdate()
        {
            _cursor.OnUpdate();
            _operator.GimmickController.OnUpdate();

            // Transition(ICharacterState nextState)���g���ڍs����������
            if (_cursor.IsClear)
            {
                _operator.StateMachine.Transition(_operator.StateMachine.IdleState);
            }
        }
        /// <summary>
        /// State�I�����Ɏ��s�����
        /// </summary>
        public void HandleEnd()
        {
            _inputManager.ActionMapChange("Player");
            _operator.IsAction = false;
            _operator.GimmickController.OnEnd();
        }

        
    }
}
