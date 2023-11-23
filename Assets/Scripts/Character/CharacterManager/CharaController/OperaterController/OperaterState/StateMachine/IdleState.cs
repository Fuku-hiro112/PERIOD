using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.OperaterState
{
    public class IdleState : IOperaterState
    {
        private OperaterController _controller;

        public IdleState(OperaterController controller)
        {
            _controller = controller;
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
            Vector3 velocity = _controller.CharacterMove.Velocity;
            // Transition(ICharacterState nextState)を使い移行条件を書く
            if (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z) > 0)
            {
                _controller.StateMachine.Transition(_controller.StateMachine.WalkState);
               
            }
            Debug.Log("立ち");
        }
        /// <summary>
        /// State終了時に実行される
        /// </summary>
        public void HandleEnd()
        {

        }
    }
}
