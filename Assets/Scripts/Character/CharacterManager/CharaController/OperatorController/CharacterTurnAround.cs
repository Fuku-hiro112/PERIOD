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
        [SerializeField] Transform _transform;
        [SerializeField] Transform _target;

        /// <summary>
        /// ����L�����N�^�[�̏��擾
        /// </summary>
        /// <param name="player"></param>
        public void InOperationCharacter(GameObject player)
        {
            _transform = player.GetComponent<Transform>();
        }

        /// <summary>
        /// �e�L�����N�^�[�̏����擾
        /// </summary>
        /// <param name="player"></param>
        /// <param name="follower"></param>
        public void InFolloewrCharacter(GameObject player, GameObject follower)
        {
            _target = player.GetComponent<Transform>();
            _transform = follower.GetComponent<Transform>();
        }

        /// <Summary>
        /// �t�H�����[���I�y���[�^�[�̂���ʒu���������\�b�h
        /// </Summary>
        public Vector3 MyTargetDirection()
        {
            // �t�H�����[���I�y���[�^�[�̕����Ɍ�������
           return _target.position - _transform.position;
        }

        /// <summary>
        /// �U�����
        /// </summary>
        /// <param name="direction></param>
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
