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
    /// ���͌��m
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
        /// �A�N�e�B�u���I���ɂȂ������̏���
        /// </summary>
        public void OnEnable()
        {
            _playerMap = _input.actions.FindActionMap("Player");
            _playerMap["Move"].performed += OnMove;
            _playerMap["Move"].canceled += OnMoveStop;
        }

        /// <summary>
        /// �A�N�e�B�u���I�t�ɂȂ������̏���
        /// </summary>
        public void OnDisable()
        {
            _playerMap["Move"].performed -= OnMove;
            _playerMap["Move"].canceled -= OnMoveStop;
        }

        /// <summary>
        /// �ړ����邽�߂̓��͒l��n��
        /// </summary>
        /// <param name="context"></param>
        private void OnMove(InputAction.CallbackContext context)
        {
            if (_playerMap != _input.currentActionMap)
                return;
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
            if (_playerMap != _input.currentActionMap)
                return;
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
        /// 
        public bool IsGimmickAction()
        {
            if (_playerMap != _input.currentActionMap)
                return false;
            return _playerMap["PushGimmick"].WasPressedThisFrame(); 
        }

        
        /// <summary>
        /// ����؂�ւ��{�^�����͔���
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            if (_playerMap != _input.currentActionMap)
                return false;
            return _playerMap["Change"].WasPressedThisFrame();
        }
        
        /// <summary>
        /// �U��
        /// </summary>
        /// <returns></returns>
        async public UniTaskVoid Vibration()
        {
            if (_playerMap != _input.currentActionMap)
                return;

            if (_input.devices.FirstOrDefault(x => x is IDualMotorRumble) is not IDualMotorRumble gamepad)
            {
                Debug.Log("�f�o�C�X���ڑ�");
                return;
            }

            // �U��
            Debug.Log("�R���g���[���U���J�n");
            gamepad.SetMotorSpeeds(0.5f, 0.0f);

            await UniTask.Delay(500, cancellationToken: _token);

            gamepad.SetMotorSpeeds(0.0f, 0.0f);

            Debug.Log("�R���g���[���U����~");
        }
        
        /// <summary>
        /// �J�n���̏���
        /// </summary>
        public void OnStart()
        {
            _input = GameObject.Find("Input").GetComponent<PlayerInput>();
            OnEnable();
        }
    }
}
