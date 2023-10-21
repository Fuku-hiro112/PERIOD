using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Transform _transform;
    private CharacterController _characterController;

    private Vector2 _inputMove;
    private float _verticalVelocity;
    private float _turnVelocity;


    /// <summary>
    /// 移動Action(PlayerInput側から呼ばれる)
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        // 入力値を保持しておく
        _inputMove = context.ReadValue<Vector2>();
    }


    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 操作入力と鉛直方向速度から、現在速度を計算
        var moveVelocity = new Vector3(
            _inputMove.x * _speed,
            _verticalVelocity,
            _inputMove.y * _speed
        );

        // 現在フレームの移動量を移動速度から計算
        var moveDelta = moveVelocity * Time.deltaTime;

        // CharacterControllerに移動量を指定し、オブジェクトを動かす
        _characterController.Move(moveDelta);

        if (_inputMove != Vector2.zero)
        {
            // 移動入力がある場合は、振り向き動作も行う

            // 操作入力からy軸周りの目標角度[deg]を計算
            var targetAngleY = -Mathf.Atan2(_inputMove.y, _inputMove.x)
                * Mathf.Rad2Deg + 90;

            // イージングしながら次の回転角度[deg]を計算
            var angleY = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetAngleY,
                ref _turnVelocity,
                0.1f
            );

            // オブジェクトの回転を更新
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }
}
