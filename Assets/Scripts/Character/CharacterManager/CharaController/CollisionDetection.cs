using Character.OperaterState;
using Cysharp.Threading.Tasks;
using Gimmick;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Item;
using UnityEngine;

namespace Character
{
    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField]
        private OperatorController _operater;
        private IOperatorInput _input;
        ISearcher _gimmickSearch;

        public bool _isBoy = false;

        public void OnInput(OperatorInput input)
        {
            _input = input;
            _gimmickSearch = GimmickDataManager.s_Instance;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
       
        }

        //** --------  �ȉ������蔻��  -------- **//
        private bool IsSameState(IOperatorState state) => state == _operater.StateMachine.CurrentState;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Gimmick"))
            {
                GimmickController gimmickController;
                other.gameObject.TryGetComponent(out gimmickController);

                if (gimmickController.Available == Gimmick.Character.Boy) 
                {
                    if (_isBoy)
                    {
                        //TODO: �����Ă�Ƃ������̒��g
                    }
                    if (!_isBoy)
                    {
                        //TODO: �Ԉ���Ă�Ƃ������̒��g
                    }
                }
                if (gimmickController.Available == Gimmick.Character.Engineer)
                {
                    if (!_isBoy)
                    {
                        //TODO: �����Ă�Ƃ������̒��g
                    }
                    if (_isBoy)
                    {
                        //TODO: �Ԉ���Ă�Ƃ������̒��g
                    }
                }
                if (gimmickController.Available == Gimmick.Character.Both)
                {
                    //TODO: �����Ă�Ƃ������̒��g
                }
            }
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
            Action(other).Forget();
        }
        async UniTaskVoid Action(Collider other)
        {
            if (_operater.CurrentCharacter != this.gameObject) return;
            if (IsSameState(_operater.StateMachine.GimmickState)) return;
            if (other.gameObject.CompareTag("Gimmick"))
            {
                if (_input.IsGimmickAction())
                {
                    //HACK: GimmickController��n���Ă���X�e�[�g��ύX���Ȃ��ƁAOnStart���Ă΂�Ȃ��Ǝv���܂�
                    other.transform.parent.TryGetComponent(out _operater.GimmickController);
                    _operater.StateMachine.Transition(_operater.StateMachine.GimmickState).Forget();
                    _operater.StateMachine.Transition(_operater.StateMachine.IdleState).Forget();
                }
            }
            /*
            if (other.gameObject.CompareTag(""))
            {

            }
            */
        }
        private void OnTriggerExit(Collider other)
        {

        }
    }
}

