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

�@�@�@�@
        public CharaController(OperatorController operaterController, FollowerController followerController)
        {
            _operatorController = operaterController;
            _followerController = followerController;
        }

        /// <summary>
        /// ���݂̑���character��ʒB
        /// </summary>
        /// <param name="player"></param>
        public void CharacterCurrent(GameObject player, GameObject follower)// operator���\��ꂾ��������player�ɂ��Ă���
        {
            _currentCharacter = player;
            _operatorController.CharacterChange(_currentCharacter);
            _followerController.CharacterChange( _currentCharacter, follower);
        }
    }
}
