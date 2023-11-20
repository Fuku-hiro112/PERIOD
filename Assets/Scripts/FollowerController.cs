using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;
using Unity.VisualScripting;


/// <Summary>
/// エンジニアがプレイヤーを追従する処理　参考元：https://nekojara.city/unity-smooth-damp
/// </Summary>
public class FollowerController : MonoBehaviour
{
    // ターゲット（メインキャラクター）
    [SerializeField] private Transform _operator;

    // 追従させるオブジェクト (エンジニア)
    [SerializeField] private Transform _follower;

    // 目標値に到達するまでのおおよその時間
    [SerializeField] private float _smoothTime;

    // 最高速度
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // メインキャラクターとの距離
    [SerializeField] private float _distanceFromOperator; 

    // 現在速度(SmoothDampの計算のために必要)
    private Vector3 _currentVelocity = Vector3.zero;

    private Vector3 _direction;

    // CharacterMoveをインスタンス
    CharacterMove CharacterMove= new CharacterMove(0f, 0f, float.PositiveInfinity);

    // x、y、z座標をメインキャラクターの座標に追従させる処理
    private void Update()
    {
        MoveFollower();
        FaceOperator();
    }


    /// <Summary>
    /// フォロワーがオペレーターを追従するメソッド
    /// </Summary>
    private void MoveFollower()
    {
        // オペレーターーの位置
        Vector3 targetPosition = _operator.position;

        // フォロワーの位置
        Vector3 currentPosition = _follower.position;

        float distance = Vector3.Distance(targetPosition, currentPosition);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // メインキャラクターとの距離を一定に保つ
        Vector3 targetPositionAdjusted = targetPosition - direction * _distanceFromOperator;

        // 一定の距離に近づくまでメインキャラクターを追う
        if (distance > _distanceFromOperator)
        {
            // 次フレームの位置を計算（SmoothDampを各軸に適用）
            float newX = Mathf.SmoothDamp(currentPosition.x, targetPositionAdjusted.x, ref _currentVelocity.x, _smoothTime, _maxSpeed);
            float newY = Mathf.SmoothDamp(currentPosition.y, targetPositionAdjusted.y, ref _currentVelocity.y, _smoothTime, _maxSpeed);
            float newZ = Mathf.SmoothDamp(currentPosition.z, targetPositionAdjusted.z, ref _currentVelocity.z, _smoothTime, _maxSpeed);

            // 現在位置の更新
            _follower.position = new Vector3(newX, newY, newZ);
        }
        else // 一定距離まで近づくと止まる
        {
            _currentVelocity = Vector3.zero;
        }
    }


    /// <Summary>
    /// フォロワーがオペレーターのいる位置を向くメソッド
    /// </Summary>
    private void FaceOperator()
    {
        // フォロワーをオペレーターの方向に向かせる
        Vector3 lookDirection = _operator.position - _follower.position;
        if (lookDirection != Vector3.zero)
        {
            var targetAngleY = -Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + 90;
        }
    }
}
    











