using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;
using Unity.VisualScripting;


/// <summary>
/// �G���W�j�A���v���C���[��Ǐ]���鏈���@�Q�l���Fhttps://nekojara.city/unity-smooth-damp
/// </summary>
public class FollowerController : MonoBehaviour
{

    [SerializeField] private Transform _operater;

    [SerializeField] private Transform _follower;

    // �ڕW�l�ɓ��B����܂ł̂����悻�̎���
    [SerializeField] private float _smoothTime;

    // �ō����x
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // ���C���L�����N�^�[�Ƃ̋���
    [SerializeField] private float _distanceFromOperater;

    // ���ݑ��x(SmoothDamp�̌v�Z�̂��߂ɕK�v)
    private Vector3 _currentVelocity = Vector3.zero;

    // �G���W�j�A�̌������v�Z����̂ɕK�v
    Vector3 _direction;



    // x�Ay�Az���W�����C���L�����N�^�[�̍��W�ɒǏ]�����鏈��
    private void Update()
    {
        // ���C���L�����N�^�[�̈ʒu
        Vector3 targetPosition = _operater.position;

        // �G���W�j�A�̈ʒu
        Vector3 currentPosition = _follower.position;

        float distance = Vector3.Distance(targetPosition, currentPosition);
        Vector3 direction = (targetPosition - currentPosition).normalized;

        // ���C���L�����N�^�[�Ƃ̋��������ɕۂ�
        Vector3 targetPositionAdjusted = targetPosition - direction * _distanceFromOperater;


        // ���̋����ɋ߂Â��܂Ń��C���L�����N�^�[��ǂ�
        if (distance > _distanceFromOperater)
        {
            // ���t���[���̈ʒu���v�Z�iSmoothDamp���e���ɓK�p�j
            float newX = Mathf.SmoothDamp(currentPosition.x, targetPositionAdjusted.x, ref _currentVelocity.x, _smoothTime, _maxSpeed);
            float newY = Mathf.SmoothDamp(currentPosition.y, targetPositionAdjusted.y, ref _currentVelocity.y, _smoothTime, _maxSpeed);
            float newZ = Mathf.SmoothDamp(currentPosition.z, targetPositionAdjusted.z, ref _currentVelocity.z, _smoothTime, _maxSpeed);

            // ���݈ʒu�̍X�V
            _follower.position = new Vector3(newX, newY, newZ);

            // �G���W�j�A�����C���L�����N�^�[�̕����Ɍ�������
            _direction = (targetPositionAdjusted - currentPosition).normalized;
            float targetAngleY = -Mathf.Atan2(_direction.z, _direction.x) * Mathf.Rad2Deg + 90;

            // �G���W�j�A�̌������X�V
            _follower.rotation = Quaternion.Euler(0, targetAngleY, 0);
        }
        else // ��苗���܂ŋ߂Â��Ǝ~�܂�
        {
            _currentVelocity = Vector3.zero;
        }
    }
}










