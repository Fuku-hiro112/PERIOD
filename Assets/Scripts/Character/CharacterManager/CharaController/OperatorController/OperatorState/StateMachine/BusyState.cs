using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.OperaterState
{
    public class BusyState : IOperatorState
    {
        private OperatorController _controller;

        public BusyState(OperatorController controller)
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
            // Transition(ICharacterState nextState)���g���ڍs����������
        }
        /// <summary>
        /// State�I�����Ɏ��s�����
        /// </summary>
        public async UniTask HandleEnd()
        {

        }
    }
}
