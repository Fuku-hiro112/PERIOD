using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// デッドゾーンに関する処理を行うクラスです。
/// </Summary>
public class DeadZoneController : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = new Vector3();

    [SerializeField] private float _maxSpeed;

    // スピードの増加率
    [SerializeField] private float _increaseSpeedRate;

    // 経過時間がここを超えるとスピードが増加する規定時間
    [SerializeField] private float _regulationTime;

    // 経過時間
    private float _elapsedTime;


    void Update()
    {
        MoveDeadZone();
    }


    /// <Summary>
    /// デッドゾーンの速度に関するメソッド
    /// </Summary>
    private void MoveDeadZone()
    {
        if (_elapsedTime < _regulationTime)
        {
            _elapsedTime = 0f; // 経過時間をリセットする

            // 時間経過によって速度を徐々に増加
            _velocity += Vector3.forward * _increaseSpeedRate * Time.deltaTime;
        }

        // velocityがmaxSpeedを超えないように制限
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

        // デッドゾーンの移動処理
        transform.position = transform.position + _velocity * Time.deltaTime;
    }


    /// <Summary>
    /// オペレーターやエンジニアがデッドゾーンに触れた時の処理をするメソッド
    /// </Summary>
    /// <param name="GameObject"></param>
    private void OnTriggerEnter(Collider GameObject)
    {
        if (GameObject.CompareTag("Player"))
        {
            // プレイヤーがデッドゾーンに触れた時、死亡処理
            PlayerDeath();

            Debug.Log("プレイヤー死亡"); // 死亡処理がまだないのでデバッグログしています。
        }
    }


    private void PlayerDeath()
    {
        // プレイヤーの死亡処理をここに記述
    }
}

