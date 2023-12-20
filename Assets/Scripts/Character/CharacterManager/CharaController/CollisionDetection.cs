using Character.OperaterState;
using Cysharp.Threading.Tasks;
using Gimmick;
using Gimmikc;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Item;

namespace Character
{
    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField]
        private OperatorController _operater;
        private IOperatorInput _input;
        private ISearcher _gimmickSearch;
       
     
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
            if (other.gameObject.CompareTag("Gimmick"))
            {
                if (_input.IsGimmickAction())
                {
                    //if (_operater.CurrentCharacter != this.gameObject) return; // ����Ȃ�
                    if (IsSameState(_operater.StateMachine.GimmickState)) return;
                    //HACK: GimmickController��n���Ă���X�e�[�g��ύX���Ȃ��ƁAOnStart���Ă΂�Ȃ��Ǝv���܂�
                    other.transform.parent.TryGetComponent(out _operater.GimmickController);
                    _operater.StateMachine.GimmickState.GetCollider(other);
                    _operater.StateMachine.Transition(_operater.StateMachine.GimmickState).Forget();
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

