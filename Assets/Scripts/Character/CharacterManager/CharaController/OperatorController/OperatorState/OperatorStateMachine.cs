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
        /// State�̏�����
        /// </summary>
        /// <param name="startingState"></param>
        public void Initialize(IOperatorState startingState)// OperaterController���珉��State��ݒ肷��
        {
            CurrentState = startingState;
            startingState.HandleStart();
        }
        /// <summary>
        /// ���݂�State���I�����A����State�Ɉڍs����
        /// </summary>
        /// <param name="nextState"></param>
        public�@async UniTaskVoid Transition(IOperatorState nextState)
        {
            // TODO: ���UniTask�֕ύX
            await CurrentState.HandleEnd(); 
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
