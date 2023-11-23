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
            if (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z) <= 0 )
            {
                _controller.StateMachine.Transition(_controller.StateMachine.IdleState);
                
            }
        }

        /// <summary>
        /// State終了時に実行される
        /// </summary>
        public void HandleEnd()
        {

        }
    }
}
