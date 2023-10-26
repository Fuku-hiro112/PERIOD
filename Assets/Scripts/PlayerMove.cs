using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;

interface IMovable
{
    void SetDirection(Vector3 direction);
}

public class PlayerMove : MonoBehaviour, IMovable
{
    [SerializeField] float _speed = 4f;
    Transform _transform;
    Rigidbody _rb;
    float _turnVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        // ������͂Ɖ����������x����A���ݑ��x���v�Z
        var moveVelocity = direction * _speed;
  
        // ���݃t���[���̈ړ��ʂ��ړ����x����v�Z
        var moveDelta = moveVelocity * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            
            // �ړ����͂�����ꍇ�́A�U�����������s��
            /*
            // ������͂���y������̖ڕW�p�x[deg]���v�Z
            var targetAngleY = -Mathf.Atan2(direction.y, direction.x)
                * Mathf.Rad2Deg + 90;
            
            // �C�[�W���O���Ȃ��玟�̉�]�p�x[deg]���v�Z
            var angleY = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetAngleY,
                ref _turnVelocity,
                0.1f
            );
            
            // �I�u�W�F�N�g�̉�]���X�V
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
            */
            _rb.velocity = moveVelocity;
        }
    }

}
