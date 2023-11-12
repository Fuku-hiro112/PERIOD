using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;
using Character;

public class PlayerMove : MonoBehaviour
{
    // 移動、振り向き関連
    [SerializeField] float _speed = 4f;
    float _turnVelocity;
    Vector3 _direction;
    Rigidbody _rb;
    Transform _transform;

    // 接地、段差登り関連
    [SerializeField]
    float raycastForwardDistance = 1.0f; // レイの長さ
    [SerializeField]
    LayerMask groundLayer; // 地面と判定するレイヤー
    [SerializeField] float _maxObstacleHeight = 2.0f; // 指定の高さ以下の障害物を判定する高さ4
    [SerializeField] Vector3 _raycastBottomPos = new Vector3(0f, -1.0f, 0f);
    [SerializeField] Vector3 _raycastTopPos;

    /// <summary>
    /// 操作キャラクターの情報取得
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="transform"></param>
    public void InOperationCharacter(Rigidbody rb, Transform transform)
    {
        _rb = rb;
        _transform = transform;
    }

    /// <summary>
    /// 入力方向の取得
    /// </summary>
    /// <param name="direction"></param>
    public void MoveDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
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


    /// <summary>
    /// 段差を登る
    /// </summary>
    /// <param name="transform"></param>
    void Climb()
    {
        // レイのポジションと向きを設定
        Vector3 raycastOrigin = _transform.position;
        Vector3 playerForward = _transform.forward;
        Vector3 playerDown = -_transform.up;
        _raycastTopPos = new Vector3(0f, _maxObstacleHeight, 0f);
        Vector3 raycastBottom = raycastOrigin + _raycastBottomPos;
        Vector3 raycastTop = raycastBottom + _raycastTopPos;

        // レイの用意
        Ray rayBottom = new Ray(raycastBottom, playerForward);
        Ray rayTop = new Ray(raycastTop, playerForward);
        Ray rayDown = new Ray(raycastOrigin, playerDown);
        Debug.DrawRay(raycastOrigin, playerForward * raycastForwardDistance, Color.blue);
        Debug.DrawRay(raycastBottom, playerForward * raycastForwardDistance, Color.red);

        // フラグの用意
        bool isRaycastBottom = Physics.Raycast(rayBottom, raycastForwardDistance, groundLayer);
        bool isRaycastTop = Physics.Raycast(rayTop, raycastForwardDistance, groundLayer);
        bool isGround = Physics.Raycast(rayDown, 1f, groundLayer);

        // レイキャストを実行
        if (_direction != Vector3.zero)
        {
            if (isRaycastBottom && isRaycastTop)
            {
                float stepForce = 0.1f;
                Vector3 pos = _transform.up * stepForce;
                _transform.Translate(pos);
            }
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        Movement();
        TurnAround();
        Climb();
    }
}
