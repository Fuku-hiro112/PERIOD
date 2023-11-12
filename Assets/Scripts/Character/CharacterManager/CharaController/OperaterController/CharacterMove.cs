using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using System;

namespace Character 
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CharacterMove 
    {
        // 移動、振り向き関連
        [SerializeField] float _speed = 5f;
        [SerializeField] Vector3 _direction;
        [SerializeField] Rigidbody _rb;
        [SerializeField] Transform _transform;
        [SerializeField] Vector3 _position;

        // 振り向き
        [SerializeField] private float _rotSpeed = 0.1f;
        float _turnVelocity;

        // 接地、段差登り関連
        [SerializeField]
        float raycastForwardDistance = 1.0f; // レイの長さ
        [SerializeField]
        LayerMask _groundLayer; // 地面と判定するレイヤー
        [SerializeField] float _maxObstacleHeight = 2.0f; // 指定の高さ以下の障害物を判定する高さ4
        [SerializeField] Vector3 _raycastBottomPos;
        [SerializeField] Vector3 _raycastTopPos;

        /// <summary>
        /// 操作キャラクターの情報取得
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="transform"></param>
        public void InOperationCharacter(GameObject player)
        {
            _rb = player.GetComponent<Rigidbody>();
            _transform = player.GetComponent<Transform>();
        }

        /// <summary>
        /// 入力方向の取得
        /// </summary>
        /// <param name="direction"></param>
        public void MoveDirection(Vector3 direction)
        {
            _direction = direction;
        }

        /// <summary>
        /// 現在のPlayerの動き
        /// </summary>
        public void Movement(float resistance)
        {
            // 操作入力と鉛直方向速度から、現在速度を計算
            var moveVelocity = _direction * _speed * resistance;
            _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
            _position = _transform.position;
            TurnAround();
            Climb();
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
                        _rotSpeed
                    );

                // オブジェクトの回転を更新
                _transform.rotation = Quaternion.Euler(0, angleY, 0);
            }
        }

        [SerializeField] bool isRaycastBottom;
        [SerializeField] bool isRaycastTop;

        /// <summary>
        /// 段差を登る
        /// </summary>
        /// <param name="transform"></param>
        void Climb()
        {
            // レイのポジションと向きを設定
            Vector3 raycastOrigin = _transform.position; // 原点
            Vector3 playerForward = _transform.forward; // 前
            Vector3 playerDown = -_transform.up; // 下

            _raycastTopPos = new Vector3 (0f, _maxObstacleHeight, 0f); // Posを上に
            _raycastBottomPos = new Vector3(playerForward.x, _maxObstacleHeight , playerForward.z * 0.5f); // Posを上、前に移動
            Vector3 bottomPos = new Vector3(0, -1f, 0); //足元を設定

            Vector3 raycastBottomArea = raycastOrigin + bottomPos + _raycastBottomPos; // プレイヤーを基準に
            Vector3 raycastTop = raycastOrigin + bottomPos + _raycastTopPos; // プレイヤーを基準に上限
        
            // レイの用意
            Ray rayBottom = new Ray(raycastBottomArea, playerDown);　// 範囲を設定
            Ray rayTop = new Ray(raycastTop, playerForward); // 上限を決定　

            // 後で消します　Rayを可視化
            Debug.DrawRay(raycastTop, playerForward * raycastForwardDistance, Color.blue);
            Debug.DrawRay(raycastBottomArea, playerDown * _maxObstacleHeight, Color.red);

            _groundLayer = LayerMask.GetMask("Ground");
            isRaycastBottom = Physics.Raycast(rayBottom, _maxObstacleHeight - 0.01f, _groundLayer);
            isRaycastTop = Physics.Raycast(rayTop, raycastForwardDistance, _groundLayer);

            // レイキャストを実行
            if (_direction != Vector3.zero )
            {
                if(isRaycastBottom && !isRaycastTop)
                {
                    Debug.Log("jannpu");
                    float stepForce = 0.1f;
                    Vector3 pos = _transform.up * stepForce;
                    _transform.Translate(pos);
                }
            }
        }
    }
}
