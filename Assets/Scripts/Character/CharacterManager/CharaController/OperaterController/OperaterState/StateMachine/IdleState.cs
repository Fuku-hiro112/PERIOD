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
        /// State開始時に実行される
        /// </summary>
        public void HandleStart()
        {


        }
        /// <summary>
        /// フレーム単位で実行される、新しい状態に移行するための条件も書く
        /// </summary>
        public void HandleUpdate()
        {
            // Transition(ICharacterState nextState)を使い移行条件を書く
        }
        /// <summary>
        /// State終了時に実行される
        /// </summary>
        public void HandleEnd()
        {

        }
    }
}
