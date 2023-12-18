using Character;
using Cysharp.Threading.Tasks;
using Gimmikc;
using Input;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gimmick
{
    // �M�~�b�N���̃J�[�\���̈ړ��A�y�ѓ����蔻��
    public class CursorController : MonoBehaviour
    {
        [SerializeField]
        private CursorInput _cursorInput;
        [SerializeField]
        private InputManager _inputManager;
        [SerializeField]
        private CameraManager _cameraManager;
        [SerializeField]
        private float _speed = 1;
        [SerializeField]
        private float _dashMagnification = 2;
        private Image _image;

        // �����ʒu
        public Vector3 StartPosition = Vector3.zero;
        public bool IsClear { get; set; }

        private void Awake()
        {
            TryGetComponent(out _image);
            _image.enabled = false;
            _inputManager = GameObject.Find("Input").GetComponent<InputManager>();
        }
        
       
        /// <summary>
        /// �X�^�[�g��
        /// </summary>
        public void OnStart(int id)
        {
            // tag�̗��p+�����͈͂����߂邽�߁ADatabase�I�u�W�F�N�g�̎q�I�u�W�F�N�g����T���悤�ɂ��Ă���
            ISearcher iSearchable =
                GameObject.FindGameObjectWithTag("DataBase").
                transform.Find("GimmickDataManager").GetComponent<GimmickDataManager>();
            
            _image.enabled = true;
            StartPosition = iSearchable.SearchPosition(id);
            // �J�[�\�����X�^�[�g�n�_��
            this.transform.position = StartPosition;
            _inputManager.ActionMapChange("UI");
            IsClear = false;
        }

        /// <summary>
        /// Update��
        /// </summary>
        public void OnUpdate()
        {
            // �ړ��l�̌v�Z
            Vector3 moveValue = _cursorInput.MoveValue * Time.deltaTime * _speed;
            
            // �_�b�V������
            /*if (_cursorInput.IsGimmickAction())
            {
                moveValue *= _dashMagnification;
            }*/

            //�ړ� 
            this.gameObject.transform.position += moveValue;// �݌v�I�ɕʂ̏ꏊ����Ăяo���悤�ɂ��Ă��邽��,�ǂ̃I�u�W�F�N�g��������Ȃ��Ȃ�ƍl������,this.gameobject�Ŗ������Ă���
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                // �����n�_��
                this.transform.position = StartPosition;
                //TODO: �Q�[���p�b�g��U�� and ��ʂ̗h�� and �T�E���h�@�ʃN���X����Ăяo����
                _cursorInput.Vibration().Forget();
                _cameraManager.Shake().Forget();

            }
            /*else if (other.gameObject.CompareTag("CheckPoint"))// ����H
            //{
            //    //TODO: �`�F�b�N�|�C���g���B����
            //    // ���̃`�F�b�N�|�C���g����Ȃ�
            }*/
            else if (other.gameObject.CompareTag("Finish"))
            {
                Debug.Log("�M�~�b�N�I��");
                //TODO: �M�~�b�N�I�������i����΁j
                _inputManager.ActionMapChange("Player");
                _image.enabled = false;
                IsClear = true;
           �@�@ // �ǉ�
            }
        }
    }
}
