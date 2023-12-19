using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    /// <Summary>
    /// �L�����N�^�[�̈ړ����Ǘ�����N���X
    /// </Summary>
    public class FollowerMove
    {
        // CharacterMove�R���X�g���N�^�̈�����������̂ɕK�v ��
        private float _distanceFromOperator, _smoothTime, _maxSpeed;
        Vector3 _currentVelocity;
        Transform _operatorTransfrom;
        Transform _followerTransfrom;

        // �R���X�g���N�^���`����
        public FollowerMove(float distanceFromOperator, float smoothTime, float maxSpeed)
        {
            _distanceFromOperator = distanceFromOperator;
            _smoothTime = smoothTime;
            _maxSpeed = maxSpeed;
        }

        /// <summary>
        /// �e�L�����N�^�[�̏����擾
        /// </summary>
        /// <param name="operater"></param>
        /// <param name="follower"></param>
        public void InFolloewrCharacter(GameObject operater ,GameObject follower)
        {
            _operatorTransfrom = operater.GetComponent<Transform>();
            _followerTransfrom = follower.GetComponent<Transform>();
        }

        /// <Summary>
        /// �t�H�����[���I�y���[�^�[��Ǐ]���郁�\�b�h
        /// </Summary>
        public void MoveFollower()
        {
            // ���C���L�����N�^�[�̈ʒu
            Vector3 targetPosition = _operatorTransfrom.position;

            // �G���W�j�A�̈ʒu
            Vector3 currentPosition = _followerTransfrom.position;

            float distance = Vector3.Distance(targetPosition, currentPosition);
            Vector3 direction = (targetPosition - currentPosition).normalized;

            // ���C���L�����N�^�[�Ƃ̋��������ɕۂ�
            Vector3 targetPositionAdjusted = targetPosition - direction * _distanceFromOperator;

            // ���̋����ɋ߂Â��܂Ń��C���L�����N�^�[��ǂ�
            if (distance > _distanceFromOperator)
            {
                // ���t���[���̈ʒu���v�Z�iSmoothDamp���e���ɓK�p�j
                float newX = Mathf.SmoothDamp(currentPosition.x, targetPositionAdjusted.x, ref _currentVelocity.x, _smoothTime, _maxSpeed);
                float newY = _followerTransfrom.position.y;
                float newZ = Mathf.SmoothDamp(currentPosition.z, targetPositionAdjusted.z, ref _currentVelocity.z, _smoothTime, _maxSpeed);

                // ���݈ʒu�̍X�V
                _followerTransfrom.position = new Vector3(newX, newY, newZ);

            }
            else // ��苗���܂ŋ߂Â��Ǝ~�܂�
            {
                _currentVelocity.x = 0f;
                _currentVelocity.z = 0f;
            }
        }
    }
}
