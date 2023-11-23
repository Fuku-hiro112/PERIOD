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
        /// Stateの初期化
        /// </summary>
        /// <param name="startingState"></param>
        public void Initialize(IOperaterState startingState)// OperaterControllerから初期Stateを設定する
        {
            CurrentState = startingState;
            startingState.HandleStart();
        }
        /// <summary>
        /// 現在のStateを終了し、次のStateに移行する
        /// </summary>
        /// <param name="nextState"></param>
        public void Transition(IOperaterState nextState)
        {
            CurrentState.HandleEnd();
            CurrentState = nextState;
            nextState.HandleStart();
        }
        /// <summary>
        /// 現在のStateのUpdate処理を毎フレーム呼び出す
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
