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
    
    // �ړ�����
    void OnMove(InputAction.CallbackContext context)
    {
        // �X�e�B�b�N���͒l��n��
        _inputMove = context.ReadValue<Vector2>();
        var direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        _move.SetDirection(direction);
    }

    // �ړ���~����
    void OnMoveStop(InputAction.CallbackContext context)
    {
        // �X�e�B�b�N���͒l��n��
        _inputMove = Vector2.zero;
        var direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        _move.SetDirection(direction);
    }
    
    private void OnStart()
    {
        _input = _component.GetComponent<PlayerInput>();

    }
    
}
