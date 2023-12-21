using Cysharp.Threading.Tasks;
using Gimmick;
using System;
using UnityEngine;

namespace Character.OperaterState
{
    [Serializable]
    public class GimmickState : IOperatorState
    {
        private OperatorController _operator;
        [SerializeField]
        private CursorController _cursor;
        private Collider _other = null;
        private GimmickSourceDataBase _dataBase;


        public GimmickState(OperatorController operatorController)
        {
            _operator = operatorController;

            GameObject.FindGameObjectWithTag("GimmickCanvas").
                transform.Find("Cursor").TryGetComponent(out _cursor);
        }
        /// <summary>
        /// State開始時に実行される
        /// </summary>
        public void HandleStart()
        {
            int gimmickID = _other.transform.parent.gameObject.GetComponent<GimmickController>().GimmickID;
            _dataBase = GimmickDataManager.s_Instance.SearchData(gimmickID);
            string characterName =_dataBase.AvailableCharacterName;

            if (characterName == "" || _operator.CurrentCharacter.name == characterName)// ""の場合は共通処理
            {
                _operator.IsAction = true;
                _cursor.OnStart(_operator.GimmickController.GimmickID);
                _operator.GimmickController.OnStart();
            }
            else
            {
                _operator.StateMachine.Transition(_operator.StateMachine.IdleState).Forget();
                Debug.Log("操作権限がございません");
            }
        }
        /// <summary>
        /// フレーム単位で実行される、新しい状態に移行するための条件も書く
        /// </summary>
        public void HandleUpdate()
        {
            _cursor.OnUpdate();
            _operator.GimmickController.OnUpdate();

            // Transition(ICharacterState nextState)を使い移行条件を書く
            if (_cursor.IsClear)
            {
                _operator.StateMachine.Transition(_operator.StateMachine.IdleState).Forget();
                _cursor.IsClear = false;
            }
        }
        /// <summary>
        /// State終了時に実行される
        /// </summary>
        public async UniTask HandleEnd() 
        {
            if (!_operator.IsAction) return;
            _operator.GimmickController.OnEnd();
            // TODO: 現在接触している、または使用したotherをどこかから持ってくる
            await _dataBase.HandleActionAsync(_other);
            _operator.IsAction = false;
            _other = null;
        }

        public void GetCollider(Collider other)
        {
            _other = other;
        }
    }
}
