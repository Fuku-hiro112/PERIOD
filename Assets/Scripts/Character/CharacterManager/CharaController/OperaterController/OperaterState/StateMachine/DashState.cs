using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.OperaterState
{
    public class DashState : IOperaterState
    {
        private OperaterController _operaterController;
        private float _resistance = 0.8f;

        public DashState(OperaterController operaterController)
        {
            _operaterController = operaterController;
        }

        public float Resistance�@=> _resistance;

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