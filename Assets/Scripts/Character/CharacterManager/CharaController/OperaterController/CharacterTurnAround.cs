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
        /// ����L�����N�^�[�̏��擾
        /// </summary>
        /// <param name="transform"></param>
        public void InOperationCharacter(GameObject player)
        {
            _transform = player.GetComponent<Transform>();
        }

        /// <summary>
        /// �U�����
        /// </summary>
        public void TurnAround(Vector3 direction)
        {
            // �ړ����͂�����ꍇ�́A�U�����������s��
            if (direction != Vector3.zero)
            {
                // ������͂���y������̖ڕW�p�x[deg]���v�Z
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
