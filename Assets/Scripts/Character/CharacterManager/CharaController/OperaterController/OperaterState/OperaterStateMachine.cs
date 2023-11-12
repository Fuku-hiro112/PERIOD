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
