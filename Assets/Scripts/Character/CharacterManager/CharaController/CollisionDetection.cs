using Character.OperaterState;
using Gimmick;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField]
        private OperatorController _operater;
        private IOperatorInput _input;

        public void OnInput(OperatorInput input)
        {
            _input = input;
        }

        //** --------  �ȉ������蔻��  -------- **//
        private bool IsSameState(IOperatorState state) => state == _operater.StateMachine.CurrentState;

        private void OnCollisionEnter(Collision other)
        {

        }
        private void OnCollisionStay(Collision other)
        {

        }
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Gimmick"))
            {
                //TODO: �e�L�X�g��\��
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Gimmick"))
            {
                //TODO: �e�L�X�g�\��
            }
            else if (other.gameObject.CompareTag("Item"))
            {
                //TODO: ���̃I�u�W�F�N�g�́u�A�C�e�����E�������v���Ă�ŃA�C�e�����擾����
                other.gameObject.GetComponent<AvailableItem>()?.PickUp();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (_operater.CurrentCharacter != this.gameObject) return;
            if (IsSameState(_operater.StateMachine.GimmickState)) return;
            if (other.gameObject.CompareTag("Gimmick"))
            {
                if (_input.IsGimmickAction())//TODO: �����̃{�^�����������Ƃ��ɕύX
                {
                    //HACK: GimmickController��n���Ă���X�e�[�g��ύX���Ȃ��ƁAOnStart���Ă΂�Ȃ��Ǝv���܂�
                    other.transform.parent.TryGetComponent(out _operater.GimmickController);
                    _operater.StateMachine.Transition(_operater.StateMachine.GimmickState);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {

        }
    }
}

