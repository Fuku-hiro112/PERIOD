using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    using Gimmick;
    using OperaterState;
    using System;
    using Unity.VisualScripting;

    public class OperatorController : MonoBehaviour
    {
        [SerializeField] private OperatorStateMachine _stateMachine;   
        [SerializeField] private OperatorMove _characterMove;
        [SerializeField] private CharacterClimb _characterClimb;
        [SerializeField] private CharacterTurnAround _characterTurnAround;
        [SerializeField] private GameObject _currentCharacter;
        private GimmickController _gimmickController; // �Ȃ��private�ɂ��Ă�H
        private IOperatorInput _IOperatorInput;

        public OperatorStateMachine StateMachine { get => _stateMachine; }
        public OperatorMove CharacterMove { get => _characterMove; }
        public GimmickController GimmickController { get => _gimmickController; }

        /// <summary>
        /// ����character�̕ύX
        /// </summary>
        /// <param name="player"></param>
        public void CharacterChange(GameObject player)
        {
            _characterMove.Movement(new Vector3(0, 1.0f, 0), 0f);
            _currentCharacter = player;
            _characterMove.InOperationCharacter(_currentCharacter);
            _characterTurnAround.InOperationCharacter(_currentCharacter);
            _characterClimb.InCharacter(_currentCharacter);
        }
        
        /// <summary>
        /// ���͏�Ԃ����
        /// </summary>
        /// <param name="input"></param>
        public void OnInput(OperatorInput input)
        {
            _IOperatorInput = input;
        }
            
        private void Awake()
        {
            _stateMachine = new OperatorStateMachine(this);
            _characterMove = new OperatorMove();
            _characterClimb = new CharacterClimb();
            _characterTurnAround = new CharacterTurnAround();
        }

        private void Start()
        {
            _stateMachine.Initialize(_stateMachine.IdleState);  
        }

        void Update()
        {
            _characterMove.Movement(_IOperatorInput.MovementValue, 1.0f);
            _characterClimb.Climb(_IOperatorInput.MovementValue);
            _characterTurnAround.TurnAround(_IOperatorInput.MovementValue);

            _stateMachine.OnUpdate();
        }
        private bool IsSameState(IOperatorState state) => state == _stateMachine.CurrentState;

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
