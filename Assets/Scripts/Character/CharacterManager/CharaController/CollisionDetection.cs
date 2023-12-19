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

        //** --------  以下当たり判定  -------- **//
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
                        //TODO: 合ってるとき処理の中身
                    }
                    if (!_isBoy)
                    {
                        //TODO: 間違ってるとき処理の中身
                    }
                }
                if (gimmickController.Available == Gimmick.Character.Engineer)
                {
                    if (!_isBoy)
                    {
                        //TODO: 合ってるとき処理の中身
                    }
                    if (_isBoy)
                    {
                        //TODO: 間違ってるとき処理の中身
                    }
                }
                if (gimmickController.Available == Gimmick.Character.Both)
                {
                    //TODO: 合ってるとき処理の中身
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
            if (_operater.CurrentCharacter != this.gameObject) return;
            if (IsSameState(_operater.StateMachine.GimmickState)) return;
            if (other.gameObject.CompareTag("Gimmick"))
            {
                if (_input.IsGimmickAction())
                {
                    //HACK: GimmickControllerを渡してからステートを変更しないと、OnStartが呼ばれないと思います
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

