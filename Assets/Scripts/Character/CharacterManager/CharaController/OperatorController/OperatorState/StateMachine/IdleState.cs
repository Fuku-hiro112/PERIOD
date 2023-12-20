using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.OperaterState
{
    public class IdleState : IOperatorState
    {
        private OperatorController _controller;

        public IdleState(OperatorController controller)
        {
            _controller = controller;
        }
        /// <summary>
        /// State�J�n���Ɏ��s�����
        /// </summary>
        public void HandleStart()
        {

        }
        /// <summary>
        /// �t���[���P�ʂŎ��s�����A�V������ԂɈڍs���邽�߂̏���������
        /// </summary>
        public void HandleUpdate()
        {
            Vector3 velocity = _controller.CharacterMove.Velocity;
            // Transition(ICharacterState nextState)���g���ڍs����������
            if (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z) > 0)
            {
                _controller.StateMachine.Transition(_controller.StateMachine.WalkState).Forget();
            }
            //Debug.Log("����");
        }
        /// <summary>
        /// State�I�����Ɏ��s�����
        /// </summary>
        public async UniTask HandleEnd()
        {

        }
    }
}
