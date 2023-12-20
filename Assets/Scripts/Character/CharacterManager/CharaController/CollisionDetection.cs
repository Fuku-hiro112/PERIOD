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

        //** --------  以下当たり判定  -------- **//
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
                //TODO: テキスト非表示
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Gimmick"))
            {
                //TODO: テキスト表示
            }
            else if (other.gameObject.CompareTag("Item"))
            {
                //TODO: そのオブジェクトの「アイテムを拾う処理」を呼んでアイテムを取得する
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
                    //if (_operater.CurrentCharacter != this.gameObject) return; // いらない
                    if (IsSameState(_operater.StateMachine.GimmickState)) return;
                    //HACK: GimmickControllerを渡してからステートを変更しないと、OnStartが呼ばれないと思います
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

