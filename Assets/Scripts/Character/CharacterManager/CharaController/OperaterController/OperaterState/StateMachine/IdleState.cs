using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.OperaterState
{
    public class IdleState : IOperaterState
    {
        private OperaterController _operaterController;

        public IdleState(OperaterController operaterController)
        {
            _operaterController = operaterController;
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
            // Transition(ICharacterState nextState)���g���ڍs����������
        }
        /// <summary>
        /// State�I�����Ɏ��s�����
        /// </summary>
        public void HandleEnd()
        {

        }
    }
}
