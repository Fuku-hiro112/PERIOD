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
    public class CharacterMove 
    {
        // �ړ��A�U������֘A
        [SerializeField] float _speed = 5f;
        [SerializeField] Vector3 _direction;
        [SerializeField] Rigidbody _rb;
        [SerializeField] Transform _transform;
        [SerializeField] Vector3 _position;�@// ��ɏ���


        /// <summary>
        /// ����L�����N�^�[�̏��擾
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="transform"></param>
        public void InOperationCharacter(GameObject player)
        {
            _rb = player.GetComponent<Rigidbody>();
            _transform = player.GetComponent<Transform>();
        }


        /// <summary>
        /// �ړ�����
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="resistance"></param>
        public void Movement(Vector3 direction, float resistance)
        {
            // ������͂Ɖ����������x����A���ݑ��x���v�Z
            var moveVelocity = direction * _speed * resistance;
            _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
            _position = _transform.position;
        }

       
    }
}
