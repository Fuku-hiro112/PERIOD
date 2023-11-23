using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Character.OperaterState
{
    public class WalkState : IOperatorState
    {
        private OperatorController _controller;

        public WalkState(OperatorController controller)
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
            if (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z) <= 0 )
            {
                _controller.StateMachine.Transition(_controller.StateMachine.IdleState);
                
            }
        }

        /// <summary>
        /// State�I�����Ɏ��s�����
        /// </summary>
        public void HandleEnd()
        {

        }
    }
}
