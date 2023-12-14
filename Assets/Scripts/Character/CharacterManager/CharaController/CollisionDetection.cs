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
            if (_operater.CurrentCharacter != this.gameObject) return;
            if (IsSameState(_operater.StateMachine.GimmickState)) return;
            if (other.gameObject.CompareTag("Gimmick"))
            {
                if (_input.IsGimmickAction())//TODO: 何かのボタンを押したときに変更
                {
                    //HACK: GimmickControllerを渡してからステートを変更しないと、OnStartが呼ばれないと思います
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

