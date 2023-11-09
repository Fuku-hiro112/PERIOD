using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// デッドゾーンの処理に関するクラス
/// </Summary>
public class DeadZoneController : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = new Vector3();

    [SerializeField] private float _maxSpeed;

    // 速度を増加する割合
    [SerializeField] private float _increaseSpeedRate;

    // 規定時間（この時間を超えたら速度を変更する）
    [SerializeField] private float _regulationTime;

    //　経過時間
    private float _elapsedTime;


    void Update()
    {
        MoveDeadZone();
    }


    /// <Summary>
    /// 時間経過でデッドゾーンの速度を上げるメソッド
    /// </Summary>
    private void MoveDeadZone()
    {
        if (_elapsedTime < _regulationTime)
        {
            _elapsedTime = 0f; // 経過時間をリセットする

            // 時間経過によって速度を徐々に増加
            _velocity += Vector3.forward * _increaseSpeedRate * Time.deltaTime;
        }

        // velocityをmaxSpeedまでに制限
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

        // デッドゾーンを進ませる
        transform.position = transform.position + _velocity * Time.deltaTime;
    }


    /// <Summary>
    /// オペレーターやフォロワーがデッドゾーンに触れたときのメソッド
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

