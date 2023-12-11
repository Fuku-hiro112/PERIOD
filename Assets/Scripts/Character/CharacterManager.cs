using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    /// <summary>
    /// character‚ÌŠÇ—
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
        [SerializeField] CollisionDetection _collisionBoy;
        [SerializeField] CollisionDetection _collisionEngineer;
        // ¶‘¶”»’è
        [SerializeField] bool isDead = false;

        public OperatorInput OperatorInput { get => _operatorInput; private set => _operatorInput = value; }
        public GameObject CurrentCharacter { get => _currentCharacter; private set => _currentCharacter = value; }



        /// <summary>
        /// Œ»İ‚Ì‘€ìcharacter‚ğ’Ê’B
        /// </summary>
        /// <param name="player"></param>
        void CharacterChange(GameObject player, GameObject follower)
        {
            CurrentCharacter = player;
            _charaController.CharacterCurrent(player, follower);
        }

        /// <summary>
        /// ‘€ìcharacterØ‚è‘Ö‚¦
        /// </summary>
        void OperaterChange()
        {
            if (_characterChange.OnChange())
            {
                if (CurrentCharacter == _boy)
                    CharacterChange(_engineer, _boy);
                else
                    CharacterChange(_boy, _engineer);
            }
        }

        void Awake()
        {
            OperatorInput = new OperatorInput();
            _characterChange = new CharacterChange(OperatorInput);
        }

        private void Start()
        {
            _charaController = new CharaController(_operatorController, _followerController);
            OperatorInput.OnStart();
            _operatorController.OnInput(OperatorInput);
            _collisionBoy.OnInput(OperatorInput);
            _collisionEngineer.OnInput(OperatorInput);
            CharacterChange(_boy, _engineer);
        }

        void Update()
        {
            OperaterChange();
        }
    }
}
