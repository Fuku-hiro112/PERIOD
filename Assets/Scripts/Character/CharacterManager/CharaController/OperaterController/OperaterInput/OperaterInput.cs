using Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    /// <summary>
    /// 入力検知
    /// </summary>
    [Serializable]
    public class OperaterInput : IOperaterInput
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Vector2 _inputMove;
        [SerializeField] Vector3 _direction;

        /// <summary>
        /// アクティブがオンになった時の処理
        /// </summary>
        public void OnEnable()
        {
            Debug.Log("スタート");
            _input.actions["Move"].performed += OnMove;
            _input.actions["Move"].canceled += OnMoveStop;
        }

        /// <summary>
        /// アクティブがオフになった時の処理
        /// </summary>
        public void OnDisable()
        {
            _input.actions["Move"].performed -= OnMove;
            _input.actions["Move"].canceled -= OnMoveStop;
        }

        /// <summary>
        /// 移動するための入力値を渡す
        /// </summary>
        /// <param name="context"></param>
        private void OnMove(InputAction.CallbackContext context)
        {
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
        [SerializeField]    
        public bool IsGimmickAction()
        {
            return _input.actions["PushGimmick"].WasPressedThisFrame(); 
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            return _input.actions["Change"].WasPressedThisFrame();
        }
        
        /// <summary>
        /// 開始時の処理
        /// </summary>
        public void OnStart()
        {
            _input = GameObject.Find("CharacterManager").GetComponent<PlayerInput>();
            OnEnable();
        }
    }
}
