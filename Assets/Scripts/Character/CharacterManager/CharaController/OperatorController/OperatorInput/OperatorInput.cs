using System;
using System.Collections;
using System.Linq;
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
        [SerializeField] Vector3 _direction;
        InputActionMap _Player;

        /// <summary>
        /// �A�N�e�B�u���I���ɂȂ������̏���
        /// </summary>
        public void OnEnable()
        {
            Debug.Log("�X�^�[�g");
            _Player = _input.actions.FindActionMap("Player");
            _input.SwitchCurrentActionMap("Player");
            _Player["Move"].performed += OnMove;
            _Player["Move"].canceled += OnMoveStop;
        }

        /// <summary>
        /// �A�N�e�B�u���I�t�ɂȂ������̏���
        /// </summary>
        public void OnDisable()
        {
            _Player["Move"].performed -= OnMove;
            _Player["Move"].canceled -= OnMoveStop;
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
        /// 
        public bool IsGimmickAction()
        {
            return _Player["PushGimmick"].WasPressedThisFrame(); 
        }

        
        /// <summary>
        /// ����؂�ւ��{�^�����͔���
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            return _Player["Change"].WasPressedThisFrame();
        }
        
        /// <summary>
        /// �U��
        /// </summary>
        /// <returns></returns>
        public IEnumerator Vibration()
        {
            if (_input.devices.FirstOrDefault(x => x is IDualMotorRumble) is not IDualMotorRumble gamepad)
            {
                Debug.Log("�f�o�C�X���ڑ�");
                yield break;
            }

            // �U��
            Debug.Log("�R���g���[���U���J�n");

            gamepad.SetMotorSpeeds(1.0f, 0.0f);
            yield return new WaitForSeconds(1.0f);

            gamepad.SetMotorSpeeds(0.0f, 1.0f);
            
            yield return new WaitForSeconds(1.0f);
            
            gamepad.SetMotorSpeeds(0.0f, 0.0f);

            Debug.Log("�R���g���[���U����~");
        }
        
        /// <summary>
        /// �J�n���̏���
        /// </summary>
        public void OnStart()
        {
            _input = GameObject.Find("CursorInput").GetComponent<PlayerInput>();
            OnEnable();
        }
    }
}
