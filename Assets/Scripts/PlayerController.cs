using Character;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private PlayerInput _input;
    private Vector2 _inputMove; // スティックの入力値
    PlayerMove _move;

    // アクション起動
    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMoveStop;
    }

    // アクションの停止
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
        _move.Direction = direction;
    }

    // 移動停止処理
    void OnMoveStop(InputAction.CallbackContext context)
    {
        // スティック入力値を渡す
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

