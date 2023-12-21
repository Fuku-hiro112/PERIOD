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
        private GimmickController _gimmickController; // なんでprivateにしてる？
        private IOperatorInput _iOperatorInput;
        [SerializeField] private GameObject _currentCharacter;
        public bool IsAction = false;

        public OperatorStateMachine StateMachine { get => _stateMachine; }
        public OperatorMove CharacterMove { get => _characterMove; }
        [NonSerialized]
        public GimmickController GimmickController;
        public GameObject CurrentCharacter { get => _currentCharacter; }
        public CharacterTurnAround CharacterTurnAround { get => _characterTurnAround; private set => _characterTurnAround = value; }
        public Animator PlayerAnimator { get => _playerAnimator; private set => _playerAnimator = value; }

        // 後に差し替え
        [SerializeField] Animator _boyAnimator;
        [SerializeField] Animator _engineerAnimator;

        [SerializeField] Animator _playerAnimator;

        /// <summary>
        /// 操作characterの変更
        /// </summary>
        /// <param name="player"></param>
        public void CharacterChange(GameObject player)
        {
            _characterMove.Movement(new Vector3(0, 1.0f, 0), 0f);
            _currentCharacter = player;
            _characterMove.InOperationCharacter(_currentCharacter);
            CharacterTurnAround.InOperationCharacter(_currentCharacter);
            _characterClimb.InCharacter(_currentCharacter);
        }
        
        /// <summary>
        /// 入力状態を入手
        /// </summary>
        /// <param name="input"></param>
        public void OnInput(OperatorInput input)
        {
            _iOperatorInput = input;
        }

        private void Awake()
        {
            _stateMachine = new OperatorStateMachine(this);
            _characterMove = new OperatorMove();
            _characterClimb = new CharacterClimb();
            CharacterTurnAround = new CharacterTurnAround();
        }

        private void Start()
        {
            _stateMachine.Initialize(_stateMachine.IdleState);
        }

        void Update()
        {
            if (!IsAction)
            {
                _characterMove.Movement(_iOperatorInput.MovementValue, 1.0f); // 移動
                //_characterClimb.Climb(_iOperatorInput.MovementValue);
                CharacterTurnAround.TurnAround(_iOperatorInput.MovementValue); // 振り向き
               
                if (Mathf.Abs(_iOperatorInput.MovementValue.z) <= Mathf.Abs(_iOperatorInput.MovementValue.x))
                   _playerAnimator.SetFloat("Speed", Mathf.Abs(_iOperatorInput.MovementValue.x));
                else
                   _playerAnimator.SetFloat("Speed", Mathf.Abs(_iOperatorInput.MovementValue.z));
                
              
            }
            else
            {
                _playerAnimator.SetFloat("Speed", 0);
                _characterMove.Movement(_iOperatorInput.MovementValue, 0f);
            }

            _stateMachine.OnUpdate();
        }
    }
}
