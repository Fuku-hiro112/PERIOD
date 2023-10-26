using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Transform _transform;
    IMovable _move;
    
    private PlayerInput _input;

    private Vector2 _inputMove;
    private float _verticalVelocity;
    private float _turnVelocity;


  
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
        var direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        _move.SetDirection(direction);

    }

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

    void OnMoveStop(InputAction.CallbackContext context)
    {
        _inputMove = Vector2.zero;
    }

    private void Awake()
    {
        _transform = transform;
        _input = GetComponent<PlayerInput>();
        TryGetComponent(out _move);
    }

    private void Update()
    {

    }
}

