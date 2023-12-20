using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    /// <Summary>
    /// キャラクターの移動を管理するクラス
    /// </Summary>
    public class FollowerMove
    {
        // CharacterMoveコンストラクタの引数を代入するのに必要 橋
        private float _distanceFromOperator, _smoothTime, _maxSpeed;
        Vector3 _currentVelocity;
        Transform _operatorTransfrom;
        Transform _followerTransfrom;

        // コンストラクタを定義する
        public FollowerMove(float distanceFromOperator, float smoothTime, float maxSpeed)
        {
            _distanceFromOperator = distanceFromOperator;
            _smoothTime = smoothTime;
            _maxSpeed = maxSpeed;
        }

        /// <summary>
        /// 各キャラクターの情報を取得
        /// </summary>
        /// <param name="operater"></param>
        /// <param name="follower"></param>
        public void InFolloewrCharacter(GameObject operater ,GameObject follower)
        {
            _operatorTransfrom = operater.GetComponent<Transform>();
            _followerTransfrom = follower.GetComponent<Transform>();
        }

        /// <Summary>
        /// フォロワーがオペレーターを追従するメソッド
        /// </Summary>
        public void MoveFollower()
        {
            // メインキャラクターの位置
            Vector3 targetPosition = _operatorTransfrom.position;

            // エンジニアの位置
            Vector3 currentPosition = _followerTransfrom.position;

            float distance = Vector3.Distance(targetPosition, currentPosition);
            Vector3 direction = (targetPosition - currentPosition).normalized;

            // メインキャラクターとの距離を一定に保つ
            Vector3 targetPositionAdjusted = targetPosition - direction * _distanceFromOperator;

            // 一定の距離に近づくまでメインキャラクターを追う
            if (distance > _distanceFromOperator)
            {
                // 次フレームの位置を計算（SmoothDampを各軸に適用）
                float newX = Mathf.SmoothDamp(currentPosition.x, targetPositionAdjusted.x, ref _currentVelocity.x, _smoothTime, _maxSpeed);
                float newY = _followerTransfrom.position.y;
                float newZ = Mathf.SmoothDamp(currentPosition.z, targetPositionAdjusted.z, ref _currentVelocity.z, _smoothTime, _maxSpeed);

                // 現在位置の更新
                _followerTransfrom.position = new Vector3(newX, newY, newZ);

            }
            else // 一定距離まで近づくと止まる
            {
                _currentVelocity.x = 0f;
                _currentVelocity.z = 0f;
            }
        }
    }
}
