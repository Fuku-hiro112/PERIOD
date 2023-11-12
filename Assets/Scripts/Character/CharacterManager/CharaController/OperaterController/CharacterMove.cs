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
    /// 
    /// </summary>
    [Serializable]
    public class CharacterMove 
    {
        // �ړ��A�U������֘A
        [SerializeField] float _speed = 5f;
        [SerializeField] Vector3 _direction;
        [SerializeField] Rigidbody _rb;
        [SerializeField] Transform _transform;
        [SerializeField] Vector3 _position;

        // �U�����
        [SerializeField] private float _rotSpeed = 0.1f;
        float _turnVelocity;

        // �ڒn�A�i���o��֘A
        [SerializeField]
        float raycastForwardDistance = 1.0f; // ���C�̒���
        [SerializeField]
        LayerMask _groundLayer; // �n�ʂƔ��肷�郌�C���[
        [SerializeField] float _maxObstacleHeight = 2.0f; // �w��̍����ȉ��̏�Q���𔻒肷�鍂��4
        [SerializeField] Vector3 _raycastBottomPos;
        [SerializeField] Vector3 _raycastTopPos;

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
        /// ���͕����̎擾
        /// </summary>
        /// <param name="direction"></param>
        public void MoveDirection(Vector3 direction)
        {
            _direction = direction;
        }

        /// <summary>
        /// ���݂�Player�̓���
        /// </summary>
        public void Movement(float resistance)
        {
            // ������͂Ɖ����������x����A���ݑ��x���v�Z
            var moveVelocity = _direction * _speed * resistance;
            _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
            _position = _transform.position;
            TurnAround();
            Climb();
        }

        /// <summary>
        /// �U�����
        /// </summary>
        void TurnAround()
        {
            // �ړ����͂�����ꍇ�́A�U�����������s��
            if (_direction != Vector3.zero)
            {
                // ������͂���y������̖ڕW�p�x[deg]���v�Z
                var targetAngleY = -Mathf.Atan2(_direction.z, _direction.x)
                    * Mathf.Rad2Deg + 90;

                var angleY = Mathf.SmoothDampAngle(
                        _transform.eulerAngles.y,
                        targetAngleY,
                        ref _turnVelocity,
                        _rotSpeed
                    );

                // �I�u�W�F�N�g�̉�]���X�V
                _transform.rotation = Quaternion.Euler(0, angleY, 0);
            }
        }

        [SerializeField] bool isRaycastBottom;
        [SerializeField] bool isRaycastTop;

        /// <summary>
        /// �i����o��
        /// </summary>
        /// <param name="transform"></param>
        void Climb()
        {
            // ���C�̃|�W�V�����ƌ�����ݒ�
            Vector3 raycastOrigin = _transform.position; // ���_
            Vector3 playerForward = _transform.forward; // �O
            Vector3 playerDown = -_transform.up; // ��

            _raycastTopPos = new Vector3 (0f, _maxObstacleHeight, 0f); // Pos�����
            _raycastBottomPos = new Vector3(playerForward.x, _maxObstacleHeight , playerForward.z * 0.5f); // Pos����A�O�Ɉړ�
            Vector3 bottomPos = new Vector3(0, -1f, 0); //������ݒ�

            Vector3 raycastBottomArea = raycastOrigin + bottomPos + _raycastBottomPos; // �v���C���[�����
            Vector3 raycastTop = raycastOrigin + bottomPos + _raycastTopPos; // �v���C���[����ɏ��
        
            // ���C�̗p��
            Ray rayBottom = new Ray(raycastBottomArea, playerDown);�@// �͈͂�ݒ�
            Ray rayTop = new Ray(raycastTop, playerForward); // ���������@

            // ��ŏ����܂��@Ray������
            Debug.DrawRay(raycastTop, playerForward * raycastForwardDistance, Color.blue);
            Debug.DrawRay(raycastBottomArea, playerDown * _maxObstacleHeight, Color.red);

            _groundLayer = LayerMask.GetMask("Ground");
            isRaycastBottom = Physics.Raycast(rayBottom, _maxObstacleHeight - 0.01f, _groundLayer);
            isRaycastTop = Physics.Raycast(rayTop, raycastForwardDistance, _groundLayer);

            // ���C�L���X�g�����s
            if (_direction != Vector3.zero )
            {
                if(isRaycastBottom && !isRaycastTop)
                {
                    Debug.Log("jannpu");
                    float stepForce = 0.1f;
                    Vector3 pos = _transform.up * stepForce;
                    _transform.Translate(pos);
                }
            }
        }
    }
}
