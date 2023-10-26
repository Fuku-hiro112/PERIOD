using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    IMovable _move;
    [SerializeField]
    private float _speed;
    private Transform _transform;
    private PlayerInput _input;
    private Vector2 _inputMove;
    
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

    // à⁄ìÆèàóù
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
        var direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        _move.SetDirection(direction);
    }

    // à⁄ìÆí‚é~èàóù
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

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Gimmick"))
        {
            if (collision.gameObject.TryGetComponent(out IGimmick gimmick))
            {
                Vector3 pos = transform.position;
               
                gimmick.DisplayButton(pos);

                gimmick.ActivateGimmick(_input.actions.FindAction("PushGimmick").WasPressedThisFrame());     
                 
                
            }
        }
    } 
}

