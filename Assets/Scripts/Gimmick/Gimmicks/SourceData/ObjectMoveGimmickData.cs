using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "ObjectMoveGimmickData", menuName = "ScriptableObject/Gimmick/ObjectMoveGimmickData")]
    public class ObjectMoveGimmickData : GimmickSourceDataBase
    {
        private CancellationTokenSource _token = new CancellationTokenSource();
        private Transform _transform;
        private Vector3 _movement;
        private float _time = 2.0f;

        public override async UniTask HandleActionAsync(Collider other)
        {
            // オブジェクトを動かす
            GameObject gimmick = other.transform.parent.gameObject;
            other.gameObject.SetActive(false);
            _transform = gimmick.transform;
            _movement = new Vector3(4,0,0);
            Vector3 pos = _transform.position;
            _movement += pos;
            await _transform.DOLocalMove(_movement,_time).WithCancellation(_token.Token);
        }
    }
}