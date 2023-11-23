using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [Serializable]
    public class CharaController
    {
        [SerializeField]
        OperatorController _operatorController;
        FollowerController _followerController;

        private GameObject _currentCharacter;

　　　　
        public CharaController(OperatorController operaterController, FollowerController followerController)
        {
            _operatorController = operaterController;
            _followerController = followerController;
        }

        /// <summary>
        /// 現在の操作characterを通達
        /// </summary>
        /// <param name="player"></param>
        public void CharacterCurrent(GameObject player, GameObject follower)// operatorが予約語だったためplayerにしている
        {
            _currentCharacter = player;
            _operatorController.CharacterChange(_currentCharacter);
            _followerController.CharacterChange( _currentCharacter, follower);
        }
    }
}
