using UnityEngine;
using UnityEngine.InputSystem;

public class CursorInput : MonoBehaviour
{
    private PlayerInput _input;
    public Vector3 MoveValue {  get; private set; }

    private void OnEnable()
    {
        TryGetComponent(out _input);
        _input.actions["Navigate"].performed += OnMove;
        _input.actions["Navigate"].canceled += OnMoveStop;
    }
    private void OnDisable()
    {
        _input.actions["Navigate"].performed -= OnMove;
        _input.actions["Navigate"].canceled -= OnMoveStop;
    }
    void OnMoveStop(InputAction.CallbackContext context)
    {
        MoveValue = Vector3.zero;
    }
    void OnMove(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();// ’l‚ÌŽæ“¾
        Debug.Log(inputValue);
        MoveValue = new Vector3(inputValue.x, inputValue.y, 0);
    }
}
