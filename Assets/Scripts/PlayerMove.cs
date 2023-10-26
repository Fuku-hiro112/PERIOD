using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;

interface IMovable
{
    void SetDirection(Vector3 direction);
}

public class PlayerMove : MonoBehaviour, IMovable
{
    [SerializeField] float _speed = 4f;
    Transform _transform;
    Rigidbody _rb;
    float _turnVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        // 操作入力と鉛直方向速度から、現在速度を計算
        var moveVelocity = direction * _speed;
  
        // 現在フレームの移動量を移動速度から計算
        var moveDelta = moveVelocity * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            
            // 移動入力がある場合は、振り向き動作も行う
            /*
            // 操作入力からy軸周りの目標角度[deg]を計算
            var targetAngleY = -Mathf.Atan2(direction.y, direction.x)
                * Mathf.Rad2Deg + 90;
            
            // イージングしながら次の回転角度[deg]を計算
            var angleY = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetAngleY,
                ref _turnVelocity,
                0.1f
            );
            
            // オブジェクトの回転を更新
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
            */
            _rb.velocity = moveVelocity;
        }
    }

}
