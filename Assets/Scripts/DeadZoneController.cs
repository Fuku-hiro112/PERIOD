using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// �f�b�h�]�[���Ɋւ��鏈�����s���N���X�ł��B
/// </Summary>
public class DeadZoneController : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = new Vector3();

    [SerializeField] private float _maxSpeed;

    // �X�s�[�h�̑�����
    [SerializeField] private float _increaseSpeedRate;

    // �o�ߎ��Ԃ������𒴂���ƃX�s�[�h����������K�莞��
    [SerializeField] private float _regulationTime;

    // �o�ߎ���
    private float _elapsedTime;


    void Update()
    {
        MoveDeadZone();
    }


    /// <Summary>
    /// �f�b�h�]�[���̑��x�Ɋւ��郁�\�b�h
    /// </Summary>
    private void MoveDeadZone()
    {
        if (_elapsedTime < _regulationTime)
        {
            _elapsedTime = 0f; // �o�ߎ��Ԃ����Z�b�g����

            // ���Ԍo�߂ɂ���đ��x�����X�ɑ���
            _velocity += Vector3.forward * _increaseSpeedRate * Time.deltaTime;
        }

        // velocity��maxSpeed�𒴂��Ȃ��悤�ɐ���
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

        // �f�b�h�]�[���̈ړ�����
        transform.position = transform.position + _velocity * Time.deltaTime;
    }


    /// <Summary>
    /// �I�y���[�^�[��G���W�j�A���f�b�h�]�[���ɐG�ꂽ���̏��������郁�\�b�h
    /// </Summary>
    /// <param name="GameObject"></param>
    private void OnTriggerEnter(Collider GameObject)
    {
        if (GameObject.CompareTag("Player"))
        {
            // �v���C���[���f�b�h�]�[���ɐG�ꂽ���A���S����
            PlayerDeath();

            Debug.Log("�v���C���[���S"); // ���S�������܂��Ȃ��̂Ńf�o�b�O���O���Ă��܂��B
        }
    }


    private void PlayerDeath()
    {
        // �v���C���[�̎��S�����������ɋL�q
    }
}

