using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;
using Unity.VisualScripting;


/// <Summary>
/// �G���W�j�A���v���C���[��Ǐ]���鏈���@�Q�l���Fhttps://nekojara.city/unity-smooth-damp
/// </Summary>
public class FollowerController : MonoBehaviour
{
    // �^�[�Q�b�g�i���C���L�����N�^�[�j
    [SerializeField] private Transform _operator;

    // �Ǐ]������I�u�W�F�N�g (�G���W�j�A)
    [SerializeField] private Transform _follower;

    // �ڕW�l�ɓ��B����܂ł̂����悻�̎���
    [SerializeField] private float _smoothTime;

    // �ō����x
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // ���C���L�����N�^�[�Ƃ̋���
    [SerializeField] private float _distanceFromOperator; 

    // ���ݑ��x(SmoothDamp�̌v�Z�̂��߂ɕK�v)
    private Vector3 _currentVelocity = Vector3.zero;

    private Vector3 _direction;

    // CharacterMove���C���X�^���X
    CharacterMove CharacterMove= new CharacterMove(0f, 0f, float.PositiveInfinity);

    // x�Ay�Az���W�����C���L�����N�^�[�̍��W�ɒǏ]�����鏈��
    private void Update()
    {
        MoveFollower();
        FaceOperator();
    }


    /// <Summary>
    /// �t�H�����[���I�y���[�^�[��Ǐ]���郁�\�b�h
    /// </Summary>
    private void MoveFollower()
    {
        // �I�y���[�^�[�[�̈ʒu
        Vector3 targetPosition = _operator.position;

        // �t�H�����[�̈ʒu
        Vector3 currentPosition = _follower.position;

        float distance = Vector3.Distance(targetPosition, currentPosition);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // ���C���L�����N�^�[�Ƃ̋��������ɕۂ�
        Vector3 targetPositionAdjusted = targetPosition - direction * _distanceFromOperator;

        // ���̋����ɋ߂Â��܂Ń��C���L�����N�^�[��ǂ�
        if (distance > _distanceFromOperator)
        {
            // ���t���[���̈ʒu���v�Z�iSmoothDamp���e���ɓK�p�j
            float newX = Mathf.SmoothDamp(currentPosition.x, targetPositionAdjusted.x, ref _currentVelocity.x, _smoothTime, _maxSpeed);
            float newY = Mathf.SmoothDamp(currentPosition.y, targetPositionAdjusted.y, ref _currentVelocity.y, _smoothTime, _maxSpeed);
            float newZ = Mathf.SmoothDamp(currentPosition.z, targetPositionAdjusted.z, ref _currentVelocity.z, _smoothTime, _maxSpeed);

            // ���݈ʒu�̍X�V
            _follower.position = new Vector3(newX, newY, newZ);
        }
        else // ��苗���܂ŋ߂Â��Ǝ~�܂�
        {
            _currentVelocity = Vector3.zero;
        }
    }


    /// <Summary>
    /// �t�H�����[���I�y���[�^�[�̂���ʒu���������\�b�h
    /// </Summary>
    private void FaceOperator()
    {
        // �t�H�����[���I�y���[�^�[�̕����Ɍ�������
        Vector3 lookDirection = _operator.position - _follower.position;
        if (lookDirection != Vector3.zero)
        {
            var targetAngleY = -Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + 90;
        }
    }
}
    











