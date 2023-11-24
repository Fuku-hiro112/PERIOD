using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class CursorInput : MonoBehaviour
    {
        private PlayerInput _input;
        public Vector3 MoveValue { get; private set; }
        public bool IsPadButtonB { get; private set; }

        public PlayerInput Input { get => _input;}// �v���p�e�B��out�ɏo���Ȃ������̂�_input�ƕ����Ă��܂�

        private void OnEnable()
        {
            TryGetComponent(out _input);
            Input.SwitchCurrentActionMap("UI");
            Input.actions["Navigate"].performed += OnMove;
            Input.actions["Navigate"].canceled += OnMoveStop;
        }
        private void OnDisable()
        {
            Input.SwitchCurrentActionMap("Player");
            Input.actions["Navigate"].performed -= OnMove;
            Input.actions["Navigate"].canceled -= OnMoveStop;
        }
        private void OnMoveStop(InputAction.CallbackContext context)
        {
            MoveValue = Vector3.zero;
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();// �l�̎擾
            Debug.Log(inputValue);
            MoveValue = new Vector3(inputValue.x, inputValue.y, 0);
        }
        void Update()
        {
            //IsPadButtonB = Gamepad.current.buttonEast.isPressed;
        }
    }
}
