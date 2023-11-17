using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using System;

namespace Character 
{
    /// <summary>
    /// characterの移動処理
    /// </summary>
    [Serializable]
    public class CharacterMove 
    {
        // 移動、振り向き関連
        [SerializeField] float _speed = 5f;
        [SerializeField] Vector3 _direction;
        [SerializeField] Rigidbody _rb;
        [SerializeField] Transform _transform;
        [SerializeField] Vector3 _position;　// 後に消去


        /// <summary>
        /// 操作キャラクターの情報取得
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="transform"></param>
        public void InOperationCharacter(GameObject player)
        {
            _rb = player.GetComponent<Rigidbody>();
            _transform = player.GetComponent<Transform>();
        }


        /// <summary>
        /// 移動処理
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="resistance"></param>
        public void Movement(Vector3 direction, float resistance)
        {
            // 操作入力と鉛直方向速度から、現在速度を計算
            var moveVelocity = direction * _speed * resistance;
            _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
            _position = _transform.position;
        }

       
    }
}
