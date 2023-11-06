using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OperaterInput
{
    IMovable _move;
    private PlayerInput _input;
    private Vector2 _inputMove;
    private Component _component = new Component();

    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMoveStop;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
    }
    
    // 移動処理
    void OnMove(InputAction.CallbackContext context)
    {
        // スティック入力値を渡す
        _inputMove = context.ReadValue<Vector2>();
        var direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        _move.SetDirection(direction);
    }

    // 移動停止処理
    void OnMoveStop(InputAction.CallbackContext context)
    {
        // スティック入力値を渡す
        _inputMove = Vector2.zero;
        var direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        _move.SetDirection(direction);
    }
    
    private void OnStart()
    {
        _input = _component.GetComponent<PlayerInput>();

    }
    
}
