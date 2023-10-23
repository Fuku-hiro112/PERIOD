using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    [Header("Z�̐��l�ő��x�ύX"), SerializeField]
    private Vector3 _velocity = new Vector3();


    void Update()
    {
       transform.position = transform.position + _velocity * Time.deltaTime;
    }


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
