using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMove 
{
    // CharacterMoveコンストラクタの引数を代入するのに必要
    private float _distanceFromOperator, _smoothTime, _maxSpeed;

    // コンストラクタを定義
    public CharacterMove(float distanceFromOperator, float smoothTime, float maxSpeed)
    {
        _distanceFromOperator = distanceFromOperator;
        _smoothTime = smoothTime;
        _maxSpeed = maxSpeed;
    }


    /// <summary>
    /// フォロワーの動きを表すメソッド
    /// </summary>
    public void MoveFollower(Transform operatorChara, Transform followerChara, ref Vector3 currentVelocity)
    {
        // メインキャラクターの位置
        Vector3 targetPosition = operatorChara.position;

        // エンジニアの位置
        Vector3 currentPosition = followerChara.position;

        float distance = Vector3.Distance(targetPosition, currentPosition);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // メインキャラクターとの距離を一定に保つ
        Vector3 targetPositionAdjusted = targetPosition - direction * _distanceFromOperator;


        // 一定の距離に近づくまでメインキャラクターを追う
        if (distance > _distanceFromOperator)
        {
            // 次フレームの位置を計算（SmoothDampを各軸に適用）
            float newX = Mathf.SmoothDamp(currentPosition.x, targetPositionAdjusted.x, ref currentVelocity.x, _smoothTime, _maxSpeed);
            float newY = Mathf.SmoothDamp(currentPosition.y, targetPositionAdjusted.y, ref currentVelocity.y, _smoothTime, _maxSpeed);
            float newZ = Mathf.SmoothDamp(currentPosition.z, targetPositionAdjusted.z, ref currentVelocity.z, _smoothTime, _maxSpeed);

            // 現在位置の更新
            followerChara.position = new Vector3(newX, newY, newZ);

        }
        else // 一定距離まで近づくと止まる
        {
            currentVelocity = Vector3.zero;
        }
    }
}
