using Character;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private PlayerInput _input;
    private Vector2 _inputMove; // �X�e�B�b�N�̓��͒l
    PlayerMove _move;

    // �A�N�V�����N��
    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMoveStop;
    }

    // �A�N�V�����̒�~
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
        _move.Direction = direction;
    }

    // �ړ���~����
    void OnMoveStop(InputAction.CallbackContext context)
    {
        // �X�e�B�b�N���͒l��n��
        _inputMove = Vector2.zero;
        var direction = new Vector3(_inputMove.x, 0, _inputMove.y);
        _move.Direction = direction;
    }
    
    private void Awake()
    {
        Application.targetFrameRate = 100;
        _input = GetComponent<PlayerInput>();
        
    }

    private void Start()
    {
        _move = GetComponent<PlayerMove>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Gimmick"))
        {
            if (collision.gameObject.TryGetComponent(out IGimmick gimmick))
            {
                Vector3 pos = transform.position;
                gimmick.DisplayButton(pos);
                gimmick.ActivateGimmick(_input.actions["PushGimmick"].WasPressedThisFrame());     
            }
        }
    }

  
}

