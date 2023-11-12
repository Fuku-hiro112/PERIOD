using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    using OperaterState;
    using System;

    public class OperaterController : MonoBehaviour
    {
        [SerializeField] private OperaterStateMachine _operaterStateMachine;
        public OperaterStateMachine OperaterStateMachine => _operaterStateMachine;

        // 
        [SerializeField] private OperaterInput _operaterinput;
        [SerializeField] private CharacterMove _characterMove;
        [SerializeField] private IOperaterInput _IOperaterInput;
        [SerializeField] private GameObject _currentCharacter;
            
        /// <summary>
        /// ‘€ìcharacter‚Ì•ÏX
        /// </summary>
        /// <param name="player"></param>
        public void CharacterChange(GameObject player)
        {
            _currentCharacter = player;
            _characterMove.InOperationCharacter(_currentCharacter);
        }

        private void Awake()
        {
            _operaterStateMachine = new OperaterStateMachine(this);
            _operaterinput = new OperaterInput();
            _characterMove = new CharacterMove();
        }
        private void Start()
        {
            _operaterStateMachine.Initialize(_operaterStateMachine.IdleState);
            _IOperaterInput = _operaterinput;
            _operaterinput.OnStart();
        }

        void Update()
        {
            _characterMove.MoveDirection(_IOperaterInput.MovementValue);
            _characterMove.Movement(1.0f);
        }
    }
}
