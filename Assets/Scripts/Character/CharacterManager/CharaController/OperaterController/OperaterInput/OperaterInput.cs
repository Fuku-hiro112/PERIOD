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
    /// ���͌��m
    /// </summary>
    [Serializable]
    public class OperaterInput : IOperaterInput
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Vector2 _inputMove;
        [SerializeField] Vector3 _direction;

        /// <summary>
        /// �A�N�e�B�u���I���ɂȂ������̏���
        /// </summary>
        public void OnEnable()
        {
            Debug.Log("�X�^�[�g");
            _input.actions["Move"].performed += OnMove;
            _input.actions["Move"].canceled += OnMoveStop;
        }

        /// <summary>
        /// �A�N�e�B�u���I�t�ɂȂ������̏���
        /// </summary>
        public void OnDisable()
        {
            _input.actions["Move"].performed -= OnMove;
            _input.actions["Move"].canceled -= OnMoveStop;
        }

        /// <summary>
        /// �ړ����邽�߂̓��͒l��n��
        /// </summary>
        /// <param name="context"></param>
        private void OnMove(InputAction.CallbackContext context)
        {
            // �X�e�B�b�N���͒l��n��
            _inputMove = context.ReadValue<Vector2>();
            _direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        }

        /// <summary>
        /// �ړ���~�̓��͒l��n��
        /// </summary>
        /// <param name="context"></param>
        private void OnMoveStop(InputAction.CallbackContext context)
        {
            // �X�e�B�b�N���͒l��n��
            _inputMove = Vector2.zero;
            _direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        }

        /// <summary>
        /// �ړ��l
        /// </summary>
        public Vector3 MovementValue
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// �M�~�b�N�A�N�V�����{�^�����͔���
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
        /// �J�n���̏���
        /// </summary>
        public void OnStart()
        {
            _input = GameObject.Find("CharacterManager").GetComponent<PlayerInput>();
            OnEnable();
        }
    }
}
