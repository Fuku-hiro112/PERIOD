using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    /// <summary>
    /// characterの管理
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        // キャラクターオブジェクト
        [SerializeField] private GameObject _boy;
        [SerializeField] private GameObject _engineer;
        // キャラクター状況
        [SerializeField] private GameObject _operator;
        [SerializeField] GameObject _follower;

        [SerializeField] private OperatorController _operatorController;
        [SerializeField] private FollowerController _followerController;
        [SerializeField] private CharaController _charaController;
        [SerializeField] private CharacterChange _characterChange;
        [SerializeField] private OperatorInput _operatorInput;
        [SerializeField] private CollisionDetection _collisionBoy;
        [SerializeField] private CollisionDetection _collisionEngineer;
        // 生存判定
        [SerializeField] private bool _engineerIsDead = false;
        [SerializeField] private bool _boyIsDead = false;
        [SerializeField] private bool _isFollow = true;

        public OperatorInput OperatorInput { get => _operatorInput; private set => _operatorInput = value; }
        public GameObject Operator { get => _operator; private set => _operator = value; }
        public GameObject Follower { get => _follower; private set => _follower = value; }

        /// <summary>
        /// 現在の操作characterを通達
        /// </summary>
        /// <param name="player"></param>
        void CharacterChange(GameObject player, GameObject follower)
        {
            Operator = player;
            Follower = follower;
            _charaController.CharacterCurrent(player, follower);
        }

        /// <summary>
        /// 操作character切り替え
        /// </summary>
        void OperaterChange()
        {
            if (_characterChange.OnChange())
            {
                if (Operator == _boy)
                {
                    CharacterChange(_engineer, _boy);
                }
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
            if (_engineerIsDead || _boyIsDead) return;
            _followerController.Follow(_isFollow);
            OperaterChange();
        }
    }
}
