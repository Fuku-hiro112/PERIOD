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
    Rigidbody _rb;
    float _turnVelocity;
    Transform _transform;
    Vector3 _direction;

    [SerializeField]
    float raycastDistance = 1.0f; // ���C�̒���
    [SerializeField]
    LayerMask groundLayer; // �n�ʂƔ��肷�郌�C���[
    bool isClimb = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction; 
    }

    /// <summary>
    /// ���݂�Player�̓���
    /// </summary>
    void Movement()
    {
        // ������͂Ɖ����������x����A���ݑ��x���v�Z
        var moveVelocity = _direction * _speed;
        _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
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
                    0.1f
                );

            // �I�u�W�F�N�g�̉�]���X�V
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }

    [SerializeField] float _maxObstacleHeight = 2.0f; // �w��̍����ȉ��̏�Q���𔻒肷�鍂��4
    [SerializeField] float _bottom = -1.0f;


    void Climb()
    {
        // ���C�̌��_��ݒ�i�ʏ�A�v���C���[�L�����N�^�[�̑����̈ʒu�j
        Vector3 raycastOrigin = _transform.position;
        raycastOrigin.y += _bottom;
        Vector3 playerForward = _transform.forward;

        // ���C�𐳖ʂɔ�΂�
        Ray ray = new Ray(raycastOrigin, playerForward);
        RaycastHit hit;

        // Ray�̎n�_������
        Debug.DrawRay(raycastOrigin, playerForward * raycastDistance, Color.blue);

        // ���C�L���X�g�����s
        if (Physics.Raycast(ray, out hit,raycastDistance, groundLayer))
        {
            float stepForce = 0;
            float posY = 0;
            if (!isClimb)
               posY = _transform.position.y;

            Debug.Log(hit.point.y�@+ "����");
            Debug.Log(posY + "�|�W�V����Y");
            
            // ��Q�����E���ɂ���A���w��̍����ȉ��ɂ���ꍇ�A������ɗ͂�������
            if (hit.point.y <= _maxObstacleHeight && posY + hit.point.y > _transform.position.y)
            {
                isClimb = true;
                stepForce = 0.2f;
                _transform.Translate(_transform.up * stepForce);
            }
            else
            {
                isClimb = false;
            }
        }
    }


    void FixedUpdate()
    {
        if (_direction != Vector3.zero)
            Climb();
    }

    void Update()
    {
        Movement();
        TurnAround();
    }
}
