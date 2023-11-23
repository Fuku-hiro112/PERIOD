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
        private GimmickController _gimmickController; // ‚È‚ñ‚Åprivate‚É‚µ‚Ä‚éH
        private IOperatorInput _IOperatorInput;
        [SerializeField] private GameObject _currentCharacter;
        public bool IsAction = false;

        public OperatorStateMachine StateMachine { get => _stateMachine; }
        public OperatorMove CharacterMove { get => _characterMove; }
        [NonSerialized]
        public GimmickController GimmickController;
        public GameObject CurrentCharacter { get => _currentCharacter; }

        /// <summary>
        /// ‘€ìcharacter‚Ì•ÏX
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
        /// “ü—Íó‘Ô‚ğ“üè
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
            if (!IsAction)
            {
                _characterMove.Movement(_IOperatorInput.MovementValue, 1.0f);
                _characterClimb.Climb(_IOperatorInput.MovementValue);
                _characterTurnAround.TurnAround(_IOperatorInput.MovementValue);
            }

            _stateMachine.OnUpdate();
        }
    }
}
