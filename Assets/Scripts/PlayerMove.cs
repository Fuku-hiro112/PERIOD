using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;
using Character;

public class PlayerMove : MonoBehaviour
{
    // �ړ��A�U������֘A
    [SerializeField] float _speed = 4f;
    float _turnVelocity;
    Vector3 _direction;
    Rigidbody _rb;
    Transform _transform;

    // �ڒn�A�i���o��֘A
    [SerializeField]
    float raycastForwardDistance = 1.0f; // ���C�̒���
    [SerializeField]
    LayerMask groundLayer; // �n�ʂƔ��肷�郌�C���[
    [SerializeField] float _maxObstacleHeight = 2.0f; // �w��̍����ȉ��̏�Q���𔻒肷�鍂��4
    [SerializeField] Vector3 _raycastBottomPos = new Vector3(0f, -1.0f, 0f);
    [SerializeField] Vector3 _raycastTopPos;

    /// <summary>
    /// ����L�����N�^�[�̏��擾
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="transform"></param>
    public void InOperationCharacter(Rigidbody rb, Transform transform)
    {
        _rb = rb;
        _transform = transform;
    }

    /// <summary>
    /// ���͕����̎擾
    /// </summary>
    /// <param name="direction"></param>
    public void MoveDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
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


    /// <summary>
    /// �i����o��
    /// </summary>
    /// <param name="transform"></param>
    void Climb()
    {
        // ���C�̃|�W�V�����ƌ�����ݒ�
        Vector3 raycastOrigin = _transform.position;
        Vector3 playerForward = _transform.forward;
        Vector3 playerDown = -_transform.up;
        _raycastTopPos = new Vector3(0f, _maxObstacleHeight, 0f);
        Vector3 raycastBottom = raycastOrigin + _raycastBottomPos;
        Vector3 raycastTop = raycastBottom + _raycastTopPos;

        // ���C�̗p��
        Ray rayBottom = new Ray(raycastBottom, playerForward);
        Ray rayTop = new Ray(raycastTop, playerForward);
        Ray rayDown = new Ray(raycastOrigin, playerDown);
        Debug.DrawRay(raycastOrigin, playerForward * raycastForwardDistance, Color.blue);
        Debug.DrawRay(raycastBottom, playerForward * raycastForwardDistance, Color.red);

        // �t���O�̗p��
        bool isRaycastBottom = Physics.Raycast(rayBottom, raycastForwardDistance, groundLayer);
        bool isRaycastTop = Physics.Raycast(rayTop, raycastForwardDistance, groundLayer);
        bool isGround = Physics.Raycast(rayDown, 1f, groundLayer);

        // ���C�L���X�g�����s
        if (_direction != Vector3.zero)
        {
            if (isRaycastBottom && isRaycastTop)
            {
                float stepForce = 0.1f;
                Vector3 pos = _transform.up * stepForce;
                _transform.Translate(pos);
            }
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        Movement();
        TurnAround();
        Climb();
    }
}
