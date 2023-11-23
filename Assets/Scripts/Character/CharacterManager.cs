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
        [SerializeField] OperaterController _operaterController;
        [SerializeField] CharaController _charaController;
        [SerializeField] CharacterChange _characterChange;
        [SerializeField] OperaterInput _operaterInput;

        // ��������
        [SerializeField] bool isDead = false;

        /// <summary>
        /// ���݂̑���character��ʒB
        /// </summary>
        /// <param name="player"></param>
        void CharacterChange(GameObject player)
        {
            _currentCharacter = player;
            _charaController.CharacterCurrent(player);
        }

        /// <summary>
        /// ����character�؂�ւ�
        /// </summary>
        void OperaterChange()
        {
            if (_characterChange.OnChange())
            {
                if (_currentCharacter == _boy)
                    CharacterChange(_engineer);
                else
                    CharacterChange(_boy);
            }
        }

        void Awake()
        {
            _operaterInput = new OperaterInput();
            _characterChange = new CharacterChange(_operaterInput);
        }

        private void Start()
        {
            _charaController = new CharaController(_operaterController);
            _operaterInput.OnStart();
            _operaterController.OnInput(_operaterInput);
            CharacterChange(_boy);
        }

        void Update()
        {
            OperaterChange();
        }
    }
}
