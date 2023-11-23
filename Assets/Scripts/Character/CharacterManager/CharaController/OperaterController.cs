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
        private GimmickController _gimmickController; // Ç»ÇÒÇ≈privateÇ…ÇµÇƒÇÈÅH
        private IOperaterInput _IOperaterInput;

        public OperaterStateMachine StateMachine { get => _stateMachine; }
        public CharacterMove CharacterMove { get => _characterMove; }
        public GimmickController GimmickController { get => _gimmickController; }

        /// <summary>
        /// ëÄçÏcharacterÇÃïœçX
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
            _IOperaterInput = input;
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
            
            _characterMove.Movement(_IOperaterInput.MovementValue, 1.0f);
            _characterClimb.Climb(_IOperaterInput.MovementValue);
            _characterTurnAround.TurnAround(_IOperaterInput.MovementValue);

            _stateMachine.OnUpdate();
        } 
    }
}
