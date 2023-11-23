using Gimmick;
using Character.OperaterState;
using UnityEngine;

namespace Character
{
    public class OperaterController : MonoBehaviour
    {
        [SerializeField]
        private OperaterStateMachine _stateMachine;
        public OperaterStateMachine StateMachine => _stateMachine;

        private GimmickController _gimmickController;
        public GimmickController GimmickController { get => _gimmickController; }

        private void Awake()
        {
            _stateMachine = new OperaterStateMachine(this);
        }
        private void Start()
        {
            _stateMachine.Initialize(_stateMachine.IdleState);
        }

        void Update()
        {
            _stateMachine.OnUpdate();
        }
        private void FixedUpdate()
        {
            
        }
        private bool IsSameState(IOperaterState state) => state == _stateMachine.CurrentState;

//** --------  �ȉ������蔻��  -------- **//

        private void OnCollisionEnter(Collision other)
        {
            
        }
        private void OnCollisionStay(Collision other)
        {
            
        }
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Gimmick"))
            {
                //TODO: �e�L�X�g��\��
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Gimmick"))
            {
                //TODO: �e�L�X�g�\��
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (IsSameState(_stateMachine.GimmickState)) return;
            if (other.gameObject.CompareTag("Gimmick"))
            {
                if (_stateMachine != null)//TODO: �����̃{�^�����������Ƃ��ɕύX
                {
                    //HACK: GimmickController��n���Ă���X�e�[�g��ύX���Ȃ��ƁAOnStart���Ă΂�Ȃ��Ǝv���܂�
                    other.transform.parent.TryGetComponent(out _gimmickController);
                    _stateMachine.Transition(_stateMachine.GimmickState);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            
        }
    }
}
