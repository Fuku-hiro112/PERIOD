using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    [Header("Zの数値で速度変更"), SerializeField]
    private Vector3 _velocity = new Vector3();


    void Update()
    {
       transform.position = transform.position + _velocity * Time.deltaTime;
    }


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
