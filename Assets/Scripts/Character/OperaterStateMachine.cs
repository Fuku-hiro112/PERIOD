using System;

namespace Character.OperaterState
{
    [Serializable]
    public class OperaterStateMachine
    {
        public IOperaterState CurrentState { get; private set; }

        public IdleState IdleState { get; private set; }
        public WalkState WalkState { get; private set; }
        public DashState DashState { get; private set; }

        public OperaterStateMachine(OperaterController player)
        {
            this.IdleState = new IdleState(player);
            this.WalkState = new WalkState(player);
            this.DashState = new DashState(player);
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
