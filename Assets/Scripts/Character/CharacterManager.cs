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

        void Awake()
        {
             
        }

        private void Start()
        {
            _charaController = new CharaController(_operatingController);
            CharacterChange(_boy);
        }

        void Update()
        {

        }

        void CharacterChange(GameObject player)
        {
            _currentCharacter = player;
            _charaController.CharacterCurrent(player);
        }
    }
}
