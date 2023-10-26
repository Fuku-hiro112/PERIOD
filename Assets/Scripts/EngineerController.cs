using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;


/// <summary>
/// エンジニアがプレイヤーを追従する処理　参考元：https://nekojara.city/unity-smooth-damp
/// </summary>
public class EngineerController : MonoBehaviour
{
    // ターゲット（メインキャラクター）
    [SerializeField] private Transform _target;

    // 追従させるオブジェクト
    [SerializeField] private Transform _follower;

    // 目標値に到達するまでのおおよその時間[s]
    [SerializeField] private float _smoothTime;

    // 最高速度
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // プレイヤーとの距離
    [SerializeField] private float _distanceFromTarget; 

    // 現在速度(SmoothDampの計算のために必要)
    private Vector3 _currentVelocity = Vector3.zero;



    // x、y、z座標をメインキャラクターの座標に追従させる処理
    private void Update()
    {
        // 現在位置を取得
        Vector3 currentPosition = _follower.position;

        // 目標の位置を計算
        Vector3 targetPosition = _target.position + (_target.forward * -_distanceFromTarget);

        // 次フレームの位置を計算
        Vector3 newPosition = Vector3.SmoothDamp(
            currentPosition,
            targetPosition,
            ref _currentVelocity,
            _smoothTime,
            _maxSpeed
        );

        // 現在位置を更新
        _follower.position = newPosition;

        // 移動方向に向かせる
        Vector3 lookAtDirection = targetPosition - currentPosition;
        if (lookAtDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookAtDirection.normalized);
            _follower.rotation = rotation;
        }
    }
}







