using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody _rigidbody;
    private Vector3 _movement;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// �ړ�����
    /// </summary>
    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movement = new Vector3(movementVector.x, 0.0f, movementVector.y);
    }

    void Update()
    {
        _rigidbody.velocity = _movement * _speed;

        ActionButton();
    }

    /// <summary>
    /// �A�N�V�����{�^������
    /// </summary>
    public void ActionButton()
    {
        // �Q�[���p�b�h���ڑ�����Ă��Ȃ���null�B
        if (Gamepad.current == null) return;

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("���������ꂽ�I");
        }
        if (Gamepad.current.buttonNorth.wasReleasedThisFrame)
        {
            Debug.Log("���������ꂽ�I");
        }


        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("�~�������ꂽ�I");
        }
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("�~�������ꂽ�I");
        }


        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            Debug.Log("���������ꂽ�I");
        }
        if (Gamepad.current.buttonWest.wasReleasedThisFrame)
        {
            Debug.Log("���������ꂽ�I");
        }


        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            Debug.Log("�Z�������ꂽ�I");
        }
        if (Gamepad.current.buttonEast.wasReleasedThisFrame)
        {
            Debug.Log("�Z�������ꂽ�I");
        }

    }


    /// <summary>
    /// �L�����N�^�[�ύX���� 
    /// </summary>
    /* public void ChangeCharacter()
    {
        // �Q�[���p�b�h���ڑ�����Ă��Ȃ���null�B
        if (Gamepad.current == null) return;

        if (Gamepad.current.leftShoulder.wasPressedThisFrame)
        {
            Debug.Log("L1�������ꂽ�I");
        }
        if (Gamepad.current.leftShoulder.wasReleasedThisFrame)
        {
            Debug.Log("L1�������ꂽ�I");
        }
        if (Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            Debug.Log("R1�������ꂽ�I");
        }
        if (Gamepad.current.rightShoulder.wasReleasedThisFrame)
        {
            Debug.Log("R1�������ꂽ�I");
        }
    } */


    /// <summary>
    /// �A�C�e���I������
    /// </summary>
    /* public void SelectItem()
    {
        // �Q�[���p�b�h���ڑ�����Ă��Ȃ���null�B
        if (Gamepad.current == null) return;

        if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            Debug.Log("L2�������ꂽ�I");
        }
        if (Gamepad.current.leftTrigger.wasReleasedThisFrame)
        {
            Debug.Log("L2�������ꂽ�I");
        }

        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            Debug.Log("R2�������ꂽ�I");
        }
        if (Gamepad.current.rightTrigger.wasReleasedThisFrame)
        {
            Debug.Log("R2�������ꂽ�I");
        }
    } */


    /// <summary>
    /// �A�C�e���g�p����
    /// </summary>
   /* public void UseItem()
    {
        // �S����\���{�^���̂ǂ��ɐݒ肷�邩������Ȃ������̂ł܂��ݒ肵�Ă��܂���B
    } */

}
