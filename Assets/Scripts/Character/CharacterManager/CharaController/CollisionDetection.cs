using Character.OperaterState;
using Cysharp.Threading.Tasks;
using Gimmick;
using Gimmikc;
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
        private ISearchable _gimmickSearch;
        // 後でけす
        MoveGimmickData _moveGImmick;

        public void OnInput(OperatorInput input)
        {
            _input = input;
            _gimmickSearch = GimmickDataManager.s_Instance;
        }

        private void Start()
        {
            _moveGImmick = new MoveGimmickData();
        }

        private void Update()
        {
            if (_input.IsGimmickAction())
                _moveGImmick.HandleActionAsync().Forget();
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

                    await other.gameObject.GetComponent<GimmickSourceDataBase>().HandleActionAsync();
                    _operater.StateMachine.Transition(_operater.StateMachine.IdleState).Forget();
                }
            }
            if (other.gameObject.CompareTag(""))
            {

            }
        }
        private void OnTriggerExit(Collider other)
        {

        }
    }
}

