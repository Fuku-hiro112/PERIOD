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
    Vector3 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        this._direction = direction; 
    }

    void Movement()
    {
        
        // ������͂Ɖ����������x����A���ݑ��x���v�Z
        var moveVelocity = _direction * _speed;

        // ���݃t���[���̈ړ��ʂ��ړ����x����v�Z
        var moveDelta = moveVelocity * Time.deltaTime;


        // �ړ����͂�����ꍇ�́A�U�����������s��
        
        // ������͂���y������̖ڕW�p�x[deg]���v�Z
        var targetAngleY = -Mathf.Atan2(_direction.y, _direction.x)
            * Mathf.Rad2Deg + 90;
        
       
       

        // �I�u�W�F�N�g�̉�]���X�V
       // _transform.rotation = Quaternion.Euler(0, 0, 0);
        
        _rb.velocity = moveVelocity;
    }

    void Update()
    {
        Movement();
    }

}
