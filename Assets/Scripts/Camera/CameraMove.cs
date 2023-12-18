using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove
{
    private Transform _camera;
    private Transform _target;
    private Vector3 _currentVelocity;
    private float _distance, _smoothTime, _maxSpeed;

    public CameraMove(Transform tr, float distance, float smoothTime, float maxSpeed)
    {
        _camera = tr;
        _distance = distance;
        _smoothTime = smoothTime;
        _maxSpeed = maxSpeed;
    }

    /// <summary>
    /// ターゲットを切り替え
    /// </summary>
    /// <param name="tr"></param>
    public void TargetChange(Transform tr)
    {
        _target = tr;
    }

    /// <Summary>
    /// プレイヤーを追従するメソッド
    /// </Summary>
    public void Follower()
    {
        // キャラクターの位置
        Vector3 targetPosition = _target.position;

        // カメラの位置
        Vector3 currentPosition = _camera.position;

        float distance = targetPosition.z - currentPosition.z;
        distance = Mathf.Abs(distance);

        if (distance >= 0)
        {
            // 次フレームの位置を計算（SmoothDampを各軸に適用）
            float newZ = Mathf.SmoothDamp(currentPosition.z, targetPosition.z - _distance,
                                          ref _currentVelocity.z, _smoothTime, _maxSpeed);
            // 現在位置の更新
            _camera.position = new Vector3(_camera.position.x, _camera.position.y, newZ);
        }
        else
        {
            _camera.position = new Vector3(_camera.position.x, _camera.position.y, targetPosition.z - distance);
        }
    }
}
