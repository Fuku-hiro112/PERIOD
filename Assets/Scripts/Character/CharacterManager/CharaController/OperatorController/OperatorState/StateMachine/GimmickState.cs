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
        /// State�J�n���Ɏ��s�����
        /// </summary>
        public void HandleStart()
        {
            GameObject gimmick = _other.transform.parent.gameObject;
            if (!gimmick)
            {
                Debug.Log("��쓮");
                return;
            }
            int gimmickID = gimmick.GetComponent<GimmickController>().GimmickID;
            _dataBase = GimmickDataManager.s_Instance.SearchData(gimmickID);
            string characterName =_dataBase.AvailableCharacterName;
            
            if (characterName == "" || _operator.CurrentCharacter.name == characterName)// ""�̏ꍇ�͋��ʏ���
            {
                _operator.IsAction = true;
                _cursor.OnStart(_operator.GimmickController.GimmickID);
                _operator.GimmickController.OnStart();
            }
            else
            {
                Debug.Log("���쌠�����������܂���");
                _operator.StateMachine.Transition(_operator.StateMachine.IdleState).Forget();
            }
        }
        /// <summary>
        /// �t���[���P�ʂŎ��s�����A�V������ԂɈڍs���邽�߂̏���������
        /// </summary>
        public void HandleUpdate()
        {
            _cursor.OnUpdate();
            _operator.GimmickController.OnUpdate();

            // Transition(ICharacterState nextState)���g���ڍs����������
            if (_cursor.IsClear)
            {
                _operator.StateMachine.Transition(_operator.StateMachine.IdleState).Forget();
                _cursor.IsClear = false;
            }
        }
        /// <summary>
        /// State�I�����Ɏ��s�����
        /// </summary>
        public async UniTask HandleEnd() 
        {
            if (!_operator.IsAction) return; 
            _operator.GimmickController.OnEnd();
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
