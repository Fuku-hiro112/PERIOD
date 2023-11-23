using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    using Gimmick;
    using OperaterState;
    using System;
    using Unity.VisualScripting;

    public class OperaterController : MonoBehaviour
    {
        [SerializeField] private OperaterStateMachine _stateMachine;   
        [SerializeField] private CharacterMove _characterMove;
        [SerializeField] private CharacterClimb _characterClimb;
        [SerializeField] private CharacterTurnAround _characterTurnAround;
        [SerializeField] private GameObject _currentCharacter;
        private GimmickController _gimmickController; // �Ȃ��private�ɂ��Ă�H
        private IOperaterInput _iOperaterInput;

        public OperaterStateMachine StateMachine { get => _stateMachine; }
        public CharacterMove CharacterMove { get => _characterMove; }
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
            _characterClimb.InOperationCharacter(_currentCharacter);
        }
        
        public void OnInput(OperaterInput input)
        {
            _iOperaterInput = input;
        }
            
        private void Awake()
        {
            _stateMachine = new OperaterStateMachine(this);
            _characterMove = new CharacterMove();
            _characterClimb = new CharacterClimb();
            _characterTurnAround = new CharacterTurnAround();
        }

        private void Start()
        {
            _stateMachine.Initialize(_stateMachine.IdleState);  
        }

        void Update()
        {
            _characterMove.Movement(_iOperaterInput.MovementValue, 1.0f);
            _characterClimb.Climb(_iOperaterInput.MovementValue);
            _characterTurnAround.TurnAround(_iOperaterInput.MovementValue);

            _stateMachine.OnUpdate();
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
