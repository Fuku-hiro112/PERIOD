using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;


/// <summary>
/// �G���W�j�A���v���C���[��Ǐ]���鏈���@�Q�l���Fhttps://nekojara.city/unity-smooth-damp
/// </summary>
public class EngineerController : MonoBehaviour
{
    // �^�[�Q�b�g�i���C���L�����N�^�[�j
    [SerializeField] private Transform _target;

    // �Ǐ]������I�u�W�F�N�g
    [SerializeField] private Transform _follower;

    // �ڕW�l�ɓ��B����܂ł̂����悻�̎���[s]
    [SerializeField] private float _smoothTime;

    // �ō����x
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // �v���C���[�Ƃ̋���
    [SerializeField] private float _distanceFromTarget; 

    // ���ݑ��x(SmoothDamp�̌v�Z�̂��߂ɕK�v)
    private Vector3 _currentVelocity = Vector3.zero;



    // x�Ay�Az���W�����C���L�����N�^�[�̍��W�ɒǏ]�����鏈��
    private void Update()
    {
        // ���݈ʒu���擾
        Vector3 currentPosition = _follower.position;

        // �ڕW�̈ʒu���v�Z
        Vector3 targetPosition = _target.position + (_target.forward * -_distanceFromTarget);

        // ���t���[���̈ʒu���v�Z
        Vector3 newPosition = Vector3.SmoothDamp(
            currentPosition,
            targetPosition,
            ref _currentVelocity,
            _smoothTime,
            _maxSpeed
        );

        // ���݈ʒu���X�V
        _follower.position = newPosition;

        // �ړ������Ɍ�������
        Vector3 lookAtDirection = targetPosition - currentPosition;
        if (lookAtDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookAtDirection.normalized);
            _follower.rotation = rotation;
        }
    }
}







