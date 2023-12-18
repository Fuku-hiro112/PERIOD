using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using Character;
using TMPro;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "ObjectMoveGimmickData", menuName = "ScriptableObject/Gimmick/ObjectMoveGimmickData")]
    public class ObjectMoveGimmickData : GimmickSourceDataBase
    {
        private CancellationTokenSource _token = new CancellationTokenSource();
        private Transform _transform;
        private Vector3 _movement;
        private float _time = 2.0f;
        // プレイヤーキャラクターの情報
        private CharacterManager _characterManager;
        private OperatorController _operatorController;
        private FollowerController _followerController;
        private CharacterTurnAround _operatorTurnAround;
        private CharacterTurnAround _followerTurnAround;
        private Transform _operator;
        private Transform _follower;

        public override async UniTask HandleActionAsync(Collider other)
        {
            _characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
            _operatorController = _characterManager.GetComponent<OperatorController>();
            _followerController = _characterManager.GetComponent<FollowerController>();
            _operatorTurnAround = _operatorController.CharacterTurnAround;
            _followerTurnAround = _followerController.CharacterTurnAround;
            _operator = _characterManager.Operator.GetComponent<Transform>();
            _follower = _characterManager.Follower.GetComponent<Transform>();

            // 二人
            if(!_characterManager.EngineerIsDead)
            {
                // 場所の移動
                // 動き
                // 振り向き
                // 操作不能に
                // 動かす速度
                _time = 2f;
            }
            // 一人
            else
            {
                // 場所の移動
                // 動き
                // 振り向き
                // 操作不能に
                // 動かす速度
                _time = 4f;
            }
            // オブジェクトを動かす
            GameObject gimmick = other.transform.parent.gameObject;
            other.gameObject.SetActive(false);
            _transform = gimmick.transform;
            _movement = new Vector3(2,0,0);
            Vector3 pos = _transform.position;
            _movement += pos;
            await _transform.DOLocalMove(_movement,_time).WithCancellation(_token.Token);
        }
    }
}