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
    Vector3 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        this._direction = direction; 
    }

    void Movement()
    {
        
        // 操作入力と鉛直方向速度から、現在速度を計算
        var moveVelocity = _direction * _speed;

        // 現在フレームの移動量を移動速度から計算
        var moveDelta = moveVelocity * Time.deltaTime;


        // 移動入力がある場合は、振り向き動作も行う
        
        // 操作入力からy軸周りの目標角度[deg]を計算
        var targetAngleY = -Mathf.Atan2(_direction.y, _direction.x)
            * Mathf.Rad2Deg + 90;
        
       
       

        // オブジェクトの回転を更新
       // _transform.rotation = Quaternion.Euler(0, 0, 0);
        
        _rb.velocity = moveVelocity;
    }

    void Update()
    {
        Movement();
    }

}
