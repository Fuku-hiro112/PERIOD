using System;
using UnityEngine;

namespace Character.OperaterState
{
    [Serializable]
    public class OperaterStateMachine
    {
        [SerializeField]
        private GimmickState _gimmickState;

        public IOperaterState CurrentState { get; private set; }
        public IdleState IdleState { get; private set; }
        public WalkState WalkState { get; private set; }
        public GimmickState GimmickState { get; private set; }

        public OperaterStateMachine(OperaterController player)
        {
            IdleState = new IdleState(player);
            WalkState = new WalkState(player);
            GimmickState = new GimmickState(player);
        }
        /// <summary>
        /// State�̏�����
        /// </summary>
        /// <param name="startingState"></param>
        public void Initialize(IOperaterState startingState)// OperaterController���珉��State��ݒ肷��
        {
            CurrentState = startingState;
            startingState.HandleStart();
        }
        /// <summary>
        /// ���݂�State���I�����A����State�Ɉڍs����
        /// </summary>
        /// <param name="nextState"></param>
        public void Transition(IOperaterState nextState)
        {
            CurrentState.HandleEnd();
            CurrentState = nextState;
            nextState.HandleStart();
        }
        /// <summary>
        /// ���݂�State��Update�����𖈃t���[���Ăяo��
        /// </summary>
        public void OnUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.HandleUpdate();
            }
        }
    }
}
