using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CharaController
    {
        [SerializeField]
        OperaterController _operaterController;

        private GameObject _currentCharacter;

        public CharaController(OperaterController operaterController)
        {
            _operaterController = operaterController;
        }

        /// <summary>
        /// 現在の操作characterを通達
        /// </summary>
        /// <param name="player"></param>
        public void CharacterCurrent(GameObject player)
        {
            _currentCharacter = player;
            _operaterController.CharacterChange(_currentCharacter);
        }
    }
}
