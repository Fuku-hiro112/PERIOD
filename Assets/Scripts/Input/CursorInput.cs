using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

namespace Input
{
    public class CursorInput : MonoBehaviour
    {
        private PlayerInput _input;
        private InputActionMap _uiMap;
        public Vector3 MoveValue { get; private set; }
        public bool IsPadButtonB { get; private set; }
        CancellationToken _token;

        public PlayerInput Input { get => _input;}// �v���p�e�B��out�ɏo���Ȃ������̂�_input�ƕ����Ă��܂�

        private void OnEnable()
        {
            TryGetComponent(out _input);
            _uiMap = _input.actions.FindActionMap("UI");
            _uiMap["Navigate"].performed += OnMove;
            _uiMap["Navigate"].canceled += OnMoveStop;
        }
        private void OnDisable()
        {
            _uiMap["Navigate"].performed -= OnMove;
            _uiMap["Navigate"].canceled -= OnMoveStop;
        }
        private void OnMoveStop(InputAction.CallbackContext context)
        {
            MoveValue = Vector3.zero;
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();// �l�̎擾
            MoveValue = new Vector3(inputValue.x, inputValue.y, 0);
        }
        /// <summary>
        /// �U��
        /// </summary>
        /// <returns></returns>
        async public UniTaskVoid Vibration()
        {
            if (_uiMap != _input.currentActionMap)
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

        void Update()
        {
            //IsPadButtonB = Gamepad.current.buttonEast.isPressed;
        }
    }
}
