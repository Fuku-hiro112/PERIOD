using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    using OperaterState;

    public class OperaterController : MonoBehaviour
    {
        private OperaterStateMachine _operaterStateMachine;
        public OperaterStateMachine OperaterStateMachine => _operaterStateMachine;

        private void Awake()
        {
            _operaterStateMachine = new OperaterStateMachine(this);
        }
        private void Start()
        {
            _operaterStateMachine.Initialize(_operaterStateMachine.IdleState);
        }

        void Update()
        {

        }
    }
}
