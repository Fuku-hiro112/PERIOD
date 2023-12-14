using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

namespace Character
{
    /// <summary>
    /// 入力検知
    /// </summary>
    [Serializable]
    public class OperatorInput : IOperatorInput
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Vector2 _inputMove;
        [SerializeField] private Vector3 _direction;
        private InputActionMap _playerMap;
        CancellationToken _token;

        /// <summary>
        /// アクティブがオンになった時の処理
        /// </summary>
        public void OnEnable()
        {
            _playerMap = _input.actions.FindActionMap("Player");
            _playerMap["Move"].performed += OnMove;
            _playerMap["Move"].canceled += OnMoveStop;
        }

        /// <summary>
        /// アクティブがオフになった時の処理
        /// </summary>
        public void OnDisable()
        {
            _playerMap["Move"].performed -= OnMove;
            _playerMap["Move"].canceled -= OnMoveStop;
        }

        /// <summary>
        /// 移動するための入力値を渡す
        /// </summary>
        /// <param name="context"></param>
        private void OnMove(InputAction.CallbackContext context)
        {
            if (_playerMap != _input.currentActionMap)
                return;
            // スティック入力値を渡す
            _inputMove = context.ReadValue<Vector2>();
            _direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        }

        /// <summary>
        /// 移動停止の入力値を渡す
        /// </summary>
        /// <param name="context"></param>
        private void OnMoveStop(InputAction.CallbackContext context)
        {
            if (_playerMap != _input.currentActionMap)
                return;
            // スティック入力値を渡す
            _inputMove = Vector2.zero;
            _direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        }

        /// <summary>
        /// 移動値
        /// </summary>
        public Vector3 MovementValue
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// ギミックアクションボタン入力判定
        /// </summary> 
        /// 
        public bool IsGimmickAction()
        {
            if (_playerMap != _input.currentActionMap)
                return false;
            return _playerMap["PushGimmick"].WasPressedThisFrame(); 
        }

        
        /// <summary>
        /// 操作切り替えボタン入力判定
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            if (_playerMap != _input.currentActionMap)
                return false;
            return _playerMap["Change"].WasPressedThisFrame();
        }
        
        /// <summary>
        /// 振動
        /// </summary>
        /// <returns></returns>
        async public UniTaskVoid Vibration()
        {
            if (_playerMap != _input.currentActionMap)
                return;

            if (_input.devices.FirstOrDefault(x => x is IDualMotorRumble) is not IDualMotorRumble gamepad)
            {
                Debug.Log("デバイス未接続");
                return;
            }

            // 振動
            Debug.Log("コントローラ振動開始");
            gamepad.SetMotorSpeeds(0.5f, 0.0f);

            await UniTask.Delay(500, cancellationToken: _token);

            gamepad.SetMotorSpeeds(0.0f, 0.0f);

            Debug.Log("コントローラ振動停止");
        }
        
        /// <summary>
        /// 開始時の処理
        /// </summary>
        public void OnStart()
        {
            _input = GameObject.Find("Input").GetComponent<PlayerInput>();
            OnEnable();
        }
    }
}
