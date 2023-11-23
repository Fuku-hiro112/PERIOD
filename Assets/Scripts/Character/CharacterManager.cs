using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    /// <summary>
    /// character�̊Ǘ�
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {

        [SerializeField] GameObject _boy;
        [SerializeField] GameObject _engineer;
        [SerializeField] GameObject _currentCharacter;
        [SerializeField] OperatorController _operatorController;
        [SerializeField] FollowerController _followerController;
        [SerializeField] CharaController _charaController;
        [SerializeField] CharacterChange _characterChange;
        [SerializeField] OperatorInput _operatorInput;

        // ��������
        [SerializeField] bool isDead = false;

        /// <summary>
        /// ���݂̑���character��ʒB
        /// </summary>
        /// <param name="player"></param>
        void CharacterChange(GameObject player, GameObject follower)
        {
            _currentCharacter = player;
            _charaController.CharacterCurrent(player, follower);
        }

        /// <summary>
        /// ����character�؂�ւ�
        /// </summary>
        void OperaterChange()
        {
            if (_characterChange.OnChange())
            {
                if (_currentCharacter == _boy)
                    CharacterChange(_engineer, _boy);
                else
                    CharacterChange(_boy, _engineer);
            }
        }

        void Awake()
        {
            _operatorInput = new OperatorInput();
            _characterChange = new CharacterChange(_operatorInput);
        }

        private void Start()
        {
            _charaController = new CharaController(_operatorController, _followerController);
            _operatorInput.OnStart();
            _operatorController.OnInput(_operatorInput);
            CharacterChange(_boy, _engineer);
        }

        void Update()
        {
            OperaterChange();
        }
    }
}
