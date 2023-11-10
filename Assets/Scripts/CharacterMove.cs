using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMove 
{
    // CharacterMove�R���X�g���N�^�̈�����������̂ɕK�v
    private float _distanceFromOperator, _smoothTime, _maxSpeed;

    // �R���X�g���N�^���`
    public CharacterMove(float distanceFromOperator, float smoothTime, float maxSpeed)
    {
        _distanceFromOperator = distanceFromOperator;
        _smoothTime = smoothTime;
        _maxSpeed = maxSpeed;
    }


    /// <summary>
    /// �t�H�����[�̓�����\�����\�b�h
    /// </summary>
    public void MoveFollower(Transform operatorChara, Transform followerChara, ref Vector3 currentVelocity)
    {
        // ���C���L�����N�^�[�̈ʒu
        Vector3 targetPosition = operatorChara.position;

        // �G���W�j�A�̈ʒu
        Vector3 currentPosition = followerChara.position;

        float distance = Vector3.Distance(targetPosition, currentPosition);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // ���C���L�����N�^�[�Ƃ̋��������ɕۂ�
        Vector3 targetPositionAdjusted = targetPosition - direction * _distanceFromOperator;


        // ���̋����ɋ߂Â��܂Ń��C���L�����N�^�[��ǂ�
        if (distance > _distanceFromOperator)
        {
            // ���t���[���̈ʒu���v�Z�iSmoothDamp���e���ɓK�p�j
            float newX = Mathf.SmoothDamp(currentPosition.x, targetPositionAdjusted.x, ref currentVelocity.x, _smoothTime, _maxSpeed);
            float newY = Mathf.SmoothDamp(currentPosition.y, targetPositionAdjusted.y, ref currentVelocity.y, _smoothTime, _maxSpeed);
            float newZ = Mathf.SmoothDamp(currentPosition.z, targetPositionAdjusted.z, ref currentVelocity.z, _smoothTime, _maxSpeed);

            // ���݈ʒu�̍X�V
            followerChara.position = new Vector3(newX, newY, newZ);

        }
        else // ��苗���܂ŋ߂Â��Ǝ~�܂�
        {
            currentVelocity = Vector3.zero;
        }
    }
}
