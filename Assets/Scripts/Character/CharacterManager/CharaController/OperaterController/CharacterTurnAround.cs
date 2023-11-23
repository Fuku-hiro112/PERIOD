using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Character {
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CharacterTurnAround
    {
        [SerializeField] private float _turnSpeed = 0.1f;
        float _turnVelocity;
        Transform _transform;

        /// <summary>
        /// 操作キャラクターの情報取得
        /// </summary>
        /// <param name="transform"></param>
        public void InOperationCharacter(GameObject player)
        {
            _transform = player.GetComponent<Transform>();
        }

        /// <summary>
        /// 振り向き
        /// </summary>
        public void TurnAround(Vector3 direction)
        {
            // 移動入力がある場合は、振り向き動作も行う
            if (direction != Vector3.zero)
            {
                // 操作入力からy軸周りの目標角度[deg]を計算
                var targetAngleY = -Mathf.Atan2(direction.z, direction.x)
                    * Mathf.Rad2Deg + 90;

                var angleY = Mathf.SmoothDampAngle(
                        _transform.eulerAngles.y,
                        targetAngleY,
                        ref _turnVelocity,
                        _turnSpeed
                    );

                _transform.rotation = Quaternion.Euler(0, angleY, 0);
            }
        }
    }
}
