using Cysharp.Threading.Tasks;
using System;

namespace Character.OperaterState
{
    [Serializable]
    public class OperatorStateMachine
    {
        public IOperatorState CurrentState { get; private set; }

        public IdleState IdleState { get; private set; }
        public WalkState WalkState { get; private set; }
        public GimmickState GimmickState { get; private set; }

        public OperatorStateMachine(OperatorController player)
        {
            IdleState = new IdleState(player);
            WalkState = new WalkState(player);
            GimmickState = new GimmickState(player);
        }
        /// <summary>
        /// Stateの初期化
        /// </summary>
        /// <param name="startingState"></param>
        public void Initialize(IOperatorState startingState)// OperaterControllerから初期Stateを設定する
        {
            CurrentState = startingState;
            startingState.HandleStart();
        }
        /// <summary>
        /// 現在のStateを終了し、次のStateに移行する
        /// </summary>
        /// <param name="nextState"></param>
        public　async UniTaskVoid Transition(IOperatorState nextState)
        {
            // TODO: 後にUniTaskへ変更
            await CurrentState.HandleEnd(); 
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
