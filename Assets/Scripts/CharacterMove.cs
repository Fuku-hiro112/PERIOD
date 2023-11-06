using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CharacterMove : IMovable
{
    [SerializeField] float _speed = 4f;
    Rigidbody _rb;
    float _turnVelocity;
    Transform _transform;
    Vector3 _direction;

    [SerializeField]
    float raycastDistance = 1.0f; // レイの長さ
    [SerializeField]
    LayerMask groundLayer; // 地面と判定するレイヤー
    bool isClimb = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    /// <summary>
    /// 現在のPlayerの動き
    /// </summary>
    void Movement()
    {
        // 操作入力と鉛直方向速度から、現在速度を計算
        var moveVelocity = _direction * _speed;
        _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
    }

    /// <summary>
    /// 振り向き
    /// </summary>
    void TurnAround()
    {
        // 移動入力がある場合は、振り向き動作も行う
        if (_direction != Vector3.zero)
        {
            // 操作入力からy軸周りの目標角度[deg]を計算
            var targetAngleY = -Mathf.Atan2(_direction.z, _direction.x)
                * Mathf.Rad2Deg + 90;

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

    [SerializeField] float _maxObstacleHeight = 2.0f; // 指定の高さ以下の障害物を判定する高さ4
    [SerializeField] float _bottom = -1.0f;


    void Climb()
    {
        // レイの原点を設定（通常、プレイヤーキャラクターの足元の位置）
        Vector3 raycastOrigin = _transform.position;
        raycastOrigin.y += _bottom;
        Vector3 playerForward = _transform.forward;

        // レイを正面に飛ばす
        Ray ray = new Ray(raycastOrigin, playerForward);
        RaycastHit hit;
        Debug.DrawRay(raycastOrigin, playerForward * raycastDistance, Color.blue);

        // レイキャストを実行
        if (Physics.Raycast(ray, out hit, raycastDistance, groundLayer))
        {
            float stepForce = 0;
            float posY = 0;
            if (!isClimb)
                posY = _transform.position.y;

            // 障害物が右側にあり、かつ指定の高さ以下にある場合、上方向に力を加える
            if (hit.point.y <= _maxObstacleHeight)
            {
                isClimb = true;
                stepForce = hit.point.y;
                _transform.Translate(_transform.up * stepForce);
            }
            else
            {
                isClimb = false;
            }
        }
    }


    void FixedUpdate()
    {
        if (_direction != Vector3.zero)
            Climb();
    }

    void Update()
    {
        Movement();
        TurnAround();
    }
}
