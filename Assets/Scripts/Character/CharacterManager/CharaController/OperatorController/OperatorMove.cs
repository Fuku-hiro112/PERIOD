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
    /// character�̈ړ�����
    /// </summary>
    [Serializable]
    public class OperatorMove 
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Vector3 _direction;
        [SerializeField] private Rigidbody _rb;

        public Vector3 Velocity { get => _rb.velocity; }

        /// <summary>
        /// ����L�����N�^�[�̏��擾
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="transform"></param>
        public void InOperationCharacter(GameObject player)
        {
            _rb = player.GetComponent<Rigidbody>();
        }

        /// <summary>
        /// �ړ�����
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="resistance"></param>
        public void Movement(Vector3 direction, float resistance)
        {
            if (_rb)
            {
                // ������͂Ɖ����������x����A���ݑ��x���v�Z
                var moveVelocity = direction * _speed * resistance;
                _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
            }
        }
    }
}
