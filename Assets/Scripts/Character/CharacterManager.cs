using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    /// <summary>
    /// 
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {

        [SerializeField] GameObject _boy;
        [SerializeField] GameObject _engineer;
        [SerializeField] GameObject _currentCharacter;
        [SerializeField] OperaterController _operatingController;
        [SerializeField] CharaController _charaController;
        [SerializeField] CharacterChange _characterChange;
        [SerializeField] OperaterInput _operaterInput;

        // ê∂ë∂îªíË
        [SerializeField] bool isDead = false;

        void CharacterChange(GameObject player)
        {
            _currentCharacter = player;
            _charaController.CharacterCurrent(player);
        }

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
            _charaController = new CharaController(_operatingController);
            _operaterInput.OnStart();
            _operatingController.OnInput(_operaterInput);
            CharacterChange(_boy);
        }

        void Update()
        {
            OperaterChange();
        }
    }
}
