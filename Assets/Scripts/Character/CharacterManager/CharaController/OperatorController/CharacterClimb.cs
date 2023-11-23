using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Character
{
    /// <summary>
    /// character�̓o�鏈��
    /// </summary>
    [Serializable]
    public class CharacterClimb
    {
        [SerializeField] Transform _transform;
        [SerializeField] float raycastForwardDistance = 1.0f; // ���C�̒���
        [SerializeField] float _maxObstacleHeight = 2.0f; // �w��̍����ȉ��̏�Q���𔻒肷�鍂��
        [SerializeField] LayerMask _groundLayer; // �n�ʂƔ��肷�郌�C���[
        [SerializeField] Vector3 _raycastBottomPos;
        [SerializeField] Vector3 _raycastTopPos;
        [SerializeField] bool isRaycastBottom;
        [SerializeField] bool isRaycastTop;

        /// <summary>
        /// ����L�����N�^�[�̏��擾
        /// </summary>
        /// <param name="transform"></param>
        public void InCharacter(GameObject player)
        {
            _transform = player.GetComponent<Transform>();
        }

        /// <summary>
        /// �i����o��
        /// </summary>
        /// <param name="direction"></param>
        public void Climb(Vector3 direction)
        {
            // ���C�̃|�W�V�����ƌ�����ݒ�
            Vector3 raycastOrigin = _transform.position; // ���_
            Vector3 playerForward = _transform.forward; // �O
            Vector3 playerDown = -_transform.up; // ��

            // ���C�𓊂���ʒu��ݒ�
            _raycastTopPos = new Vector3(0f, _maxObstacleHeight, 0f);
            _raycastBottomPos = new Vector3(playerForward.x * 0.5f, _maxObstacleHeight, playerForward.z * 0.5f);

            Vector3 bottomPos = new Vector3(0, -1f, 0); //������ݒ�

            // �v���C���[��Ń��C�̓�����ʒu��ݒ�
            Vector3 raycastBottomArea = raycastOrigin + bottomPos + _raycastBottomPos;
            Vector3 raycastTop = raycastOrigin + bottomPos + _raycastTopPos;

            // ���C�̗p��
            Ray rayBottom = new Ray(raycastBottomArea, playerDown); // �o��͈͂�ݒ�
            Ray rayTop = new Ray(raycastTop, playerForward); // �����ݒ�

            // ��ŏ����܂��@Ray������
            Debug.DrawRay(raycastTop, playerForward * raycastForwardDistance, Color.blue);
            Debug.DrawRay(raycastBottomArea, playerDown * _maxObstacleHeight, Color.red);

            _groundLayer = LayerMask.GetMask("Ground");
            isRaycastBottom = Physics.Raycast(rayBottom, _maxObstacleHeight - 0.01f, _groundLayer);
            isRaycastTop = Physics.Raycast(rayTop, raycastForwardDistance, _groundLayer);

            // ���C�L���X�g�����s
            if (direction != Vector3.zero)
            {
                if (isRaycastBottom && !isRaycastTop)
                {
                    float stepForce = 0.1f;
                    Vector3 pos = _transform.up * stepForce;
                    _transform.Translate(pos);
                }
            }
        }
    }
}
