using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    using OperaterState;
    using System;
    using Unity.VisualScripting;

    public class OperaterController : MonoBehaviour
    {
        [SerializeField] private OperaterStateMachine _operaterStateMachine;
        public OperaterStateMachine OperaterStateMachine => _operaterStateMachine;

        // 
        [SerializeField] private CharacterMove _characterMove;
        [SerializeField] private CharacterClimb _characterClimb;
        [SerializeField] private CharacterTurnAround _characterTurnAround;
        private IOperaterInput _IOperaterInput;
        [SerializeField] private GameObject _currentCharacter;
            
        /// <summary>
        /// ëÄçÏcharacterÇÃïœçX
        /// </summary>
        /// <param name="player"></param>
        public void CharacterChange(GameObject player)
        {
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
            _operaterStateMachine = new OperaterStateMachine(this);
            _characterMove = new CharacterMove();
            _characterClimb = new CharacterClimb();
            _characterTurnAround = new CharacterTurnAround();
        }
        private void Start()
        {
            _operaterStateMachine.Initialize(_operaterStateMachine.IdleState);
           
        }

        void Update()
        {
            _characterMove.Movement(_IOperaterInput.MovementValue, 1.0f);
            _characterClimb.Climb(_IOperaterInput.MovementValue);
            _characterTurnAround.TurnAround(_IOperaterInput.MovementValue);
        } 
    }
}
