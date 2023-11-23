using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Character
{
    /// <summary>
    /// characterの登る処理
    /// </summary>
    [Serializable]
    public class CharacterClimb
    {
        [SerializeField] Transform _transform;
        [SerializeField] float raycastForwardDistance = 1.0f; // レイの長さ
        [SerializeField] float _maxObstacleHeight = 2.0f; // 指定の高さ以下の障害物を判定する高さ
        [SerializeField] LayerMask _groundLayer; // 地面と判定するレイヤー
        [SerializeField] Vector3 _raycastBottomPos;
        [SerializeField] Vector3 _raycastTopPos;
        [SerializeField] bool isRaycastBottom;
        [SerializeField] bool isRaycastTop;

        /// <summary>
        /// 操作キャラクターの情報取得
        /// </summary>
        /// <param name="transform"></param>
        public void InCharacter(GameObject player)
        {
            _transform = player.GetComponent<Transform>();
        }

        /// <summary>
        /// 段差を登る
        /// </summary>
        /// <param name="direction"></param>
        public void Climb(Vector3 direction)
        {
            // レイのポジションと向きを設定
            Vector3 raycastOrigin = _transform.position; // 原点
            Vector3 playerForward = _transform.forward; // 前
            Vector3 playerDown = -_transform.up; // 下

            // レイを投げる位置を設定
            _raycastTopPos = new Vector3(0f, _maxObstacleHeight, 0f);
            _raycastBottomPos = new Vector3(playerForward.x * 0.5f, _maxObstacleHeight, playerForward.z * 0.5f);

            Vector3 bottomPos = new Vector3(0, -1f, 0); //足元を設定

            // プレイヤー基準でレイの投げる位置を設定
            Vector3 raycastBottomArea = raycastOrigin + bottomPos + _raycastBottomPos;
            Vector3 raycastTop = raycastOrigin + bottomPos + _raycastTopPos;

            // レイの用意
            Ray rayBottom = new Ray(raycastBottomArea, playerDown); // 登る範囲を設定
            Ray rayTop = new Ray(raycastTop, playerForward); // 上限を設定

            // 後で消します　Rayを可視化
            Debug.DrawRay(raycastTop, playerForward * raycastForwardDistance, Color.blue);
            Debug.DrawRay(raycastBottomArea, playerDown * _maxObstacleHeight, Color.red);

            _groundLayer = LayerMask.GetMask("Ground");
            isRaycastBottom = Physics.Raycast(rayBottom, _maxObstacleHeight - 0.01f, _groundLayer);
            isRaycastTop = Physics.Raycast(rayTop, raycastForwardDistance, _groundLayer);

            // レイキャストを実行
            if (direction != Vector3.zero)
            {
                if (isRaycastBottom && !isRaycastTop)
                {
                    float stepForce = 0.1f;
                    Vector3 pos = _transform.up * stepForce;
                    _transform.Translate(pos);
                }
            }
        }
    }
}
