using Character;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "MoveGimmickData", menuName = "ScriptableObject/Gimmick/MoveGimmickData")]
    public class MoveGimmickData : GimmickSourceDataBase
    {
        [SerializeField] private Vector3 _movePos;  // �ړI�n���w��
        [SerializeField] private float _high = 2f;
        //[SerializeField] private float _moveSpeed = 0.001f;
        private bool _used = false;
        private CharacterManager _characterManager;
        private Transform _transform;
        private CancellationTokenSource _token = new CancellationTokenSource();
      
       
        public override async UniTask HandleActionAsync()
        {
            //if (_used) return;
            _used = true;
            _characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
            _transform = _characterManager.CurrentCharacter.GetComponent<Transform>();
            _movePos = _transform.position;
            _movePos.y += _high;
            _movePos += _transform.forward * 1;
            float time = 0.7f;

            // ���B�_�Ɉړ�           
            Vector3 upMove = new Vector3(_transform.position.x, _movePos.y, _transform.position.z);
            await _transform.DOMove(upMove, time).WithCancellation(_token.Token);

            // ���B�_�ɂ����Ƃ�
            await _transform.DOMove(_movePos, time).WithCancellation(_token.Token);
            Debug.Log("�͂����[");
        }

    }
}
