using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;
using Unity.VisualScripting;


/// <summary>
/// エンジニアがプレイヤーを追従する処理　参考元：https://nekojara.city/unity-smooth-damp
/// </summary>
public class FollowerController : MonoBehaviour
{

    [SerializeField] private Transform _operater;

    [SerializeField] private Transform _follower;

    // 目標値に到達するまでのおおよその時間
    [SerializeField] private float _smoothTime;

    // 最高速度
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // メインキャラクターとの距離
    [SerializeField] private float _distanceFromOperater;

    // 現在速度(SmoothDampの計算のために必要)
    private Vector3 _currentVelocity = Vector3.zero;

    // エンジニアの向きを計算するのに必要
    Vector3 _direction;



    // x、y、z座標をメインキャラクターの座標に追従させる処理
    private void Update()
    {
        // メインキャラクターの位置
        Vector3 targetPosition = _operater.position;

        // エンジニアの位置
        Vector3 currentPosition = _follower.position;

        float distance = Vector3.Distance(targetPosition, currentPosition);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // メインキャラクターとの距離を一定に保つ
        Vector3 targetPositionAdjusted = targetPosition - direction * _distanceFromOperater;


        // 一定の距離に近づくまでメインキャラクターを追う
        if (distance > _distanceFromOperater)
        {
            // 次フレームの位置を計算（SmoothDampを各軸に適用）
            float newX = Mathf.SmoothDamp(currentPosition.x, targetPositionAdjusted.x, ref _currentVelocity.x, _smoothTime, _maxSpeed);
            float newY = Mathf.SmoothDamp(currentPosition.y, targetPositionAdjusted.y, ref _currentVelocity.y, _smoothTime, _maxSpeed);
            float newZ = Mathf.SmoothDamp(currentPosition.z, targetPositionAdjusted.z, ref _currentVelocity.z, _smoothTime, _maxSpeed);

            // 現在位置の更新
            _follower.position = new Vector3(newX, newY, newZ);

            // エンジニアをメインキャラクターの方向に向かせる
            _direction = (targetPositionAdjusted - currentPosition).normalized;
            float targetAngleY = -Mathf.Atan2(_direction.z, _direction.x) * Mathf.Rad2Deg + 90;

            // エンジニアの向きを更新
            _follower.rotation = Quaternion.Euler(0, targetAngleY, 0);
        }
        else // 一定距離まで近づくと止まる
        {
            _currentVelocity = Vector3.zero;
        }
    }
}










