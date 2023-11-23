using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    public float walkSpeed; // ��������
    public float runSpeed;  // ���鑬��

    private Animator _animator;
    private Vector2 movementInput;
    private float currentSpeed;

    private bool isDeath = false;

    Vector3 Dir; //���X�e�B�b�N�̑������
    [SerializeField] float _MoveSpeed; //�ړ����x
    [SerializeField] float _Threshold; //�������l�F0.2�̂Q���z��


    void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            return;
        }
    }


    void FixedUpdate()
    {
        if (isDeath)
        {
            // Death�A�j���[�V�������Đ����̏ꍇ�A�ړ��͖����ɂ���
            return;
        }


        if (Gamepad.current == null)
        {
            return;
        }

        //�ړ�����
        float Lx = Gamepad.current.leftStick.ReadValue().x;
        float Ly = Gamepad.current.leftStick.ReadValue().y;
        Dir = new Vector3(Lx, 0, Ly);

        if (Dir.sqrMagnitude > _Threshold)
        {
            transform.position += Dir * _MoveSpeed * Time.fixedDeltaTime;
            //���������̂Q�̃x�N�g����Dot�ρi���������Ȃ�1�A���Ε����Ȃ�-1�j�����߂�
            _animator.SetFloat("Vertical", Vector3.Dot(Dir, transform.forward));
            _animator.SetFloat("Horizontal", Vector3.Dot(Dir, transform.right));
        }
        else
        {
            _animator.SetFloat("Vertical", 0.0f);
            _animator.SetFloat("Horizontal", 0.0f);
        }
    }


    void Update()
    {
        if (isDeath)
        {
            // Death�A�j���[�V�������Đ����̏ꍇ�A�ړ��͖����ɂ���
            return;
        }

        if (Gamepad.current == null)
        {
            return;
        }

        MoveAnimation();
    }

    /// <summary>
    /// Animator��Speed�̒l��Walk��Run��؂�ւ��郁�\�b�h
    /// </summary>
    void MoveAnimation()
    {
        // Input System����̓��͂��擾
        movementInput = InputSystem.GetDevice<Gamepad>().leftStick.ReadValue();

        // ���͂��瑬�x���v�Z
        currentSpeed = movementInput.magnitude;

        // �A�j���[�^�[�̃p�����[�^�[��ݒ�
        _animator.SetFloat("Speed", currentSpeed);


        // �����A�j���[�V�����Ƒ���A�j���[�V������؂�ւ�
        if (currentSpeed > 0.5f)
        {
            if (InputSystem.GetDevice<Gamepad>().leftStickButton.isPressed)
            {
                currentSpeed *= runSpeed; // ���鑬����K�p
            }
            else
            {
                currentSpeed *= walkSpeed; // ����������K�p
            }
        }
    }

    /// <summary>
    /// // �f�b�h�]�[���ɓ�������Death�A�j���[�V�������Đ�
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            _animator.SetTrigger("Death");
            isDeath = true;
        }
    }
}


