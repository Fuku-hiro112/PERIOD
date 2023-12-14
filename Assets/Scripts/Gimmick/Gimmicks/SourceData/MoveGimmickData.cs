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
        [SerializeField] private float _moveSpeedOrigin = 0.7f;
        private CharacterManager _characterManager;
        private OperatorController _operatorController;
        private FollowerController _followerController;
        private CharacterTurnAround _operatorTurnAround;
        private CharacterTurnAround _followerTurnAround;
        private Transform _operator;
        private Transform _follower;
        private CancellationTokenSource _token = new CancellationTokenSource();

        // ��ɃA�C�e���ɕύX
        public override async UniTask HandleActionAsync(Collider other)
        {�@
            // �L�����N�^�[�̏����擾
            _characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
            _operatorController = _characterManager.GetComponent<OperatorController>();
            _followerController = _characterManager.GetComponent<FollowerController>();
            _operatorTurnAround = _operatorController.CharacterTurnAround;
            _followerTurnAround = _followerController.CharacterTurnAround;
            _operator = _characterManager.Operator.GetComponent<Transform>();
            _follower = _characterManager.Follower.GetComponent<Transform>();

            // �K�v��
            float moveSpeed = _moveSpeedOrigin;
            Vector3 originPos = _operator.position;
            _movePos = originPos;
            _movePos.y += _high;
            _movePos += _operator.forward * 1; // TODO: ��q��ڒn���������ɂ���

            // �I�y���[�^�[
            // ���B�_�Ɉړ�           
            _operatorController.IsAction = true;
            _operatorTurnAround.TurnAround(_movePos - _operator.position);
            Vector3 upMove = new Vector3(_operator.position.x, _movePos.y, _operator.position.z);
            await _operator.DOMove(upMove, moveSpeed).WithCancellation(_token.Token);

            // ���B�_�ɂ����Ƃ�
            await _operator.DOMove(_movePos, moveSpeed).WithCancellation(_token.Token);
            _operatorController.IsAction = false;

            // �t�H�����[
            if (!_followerController.IsFollow)
                return;

            // �L�^�����ꏊ�ɐU������A�ړ��@
            _followerController.IsAction = true;
            _followerTurnAround.TurnAround(_movePos - _follower.position);
            moveSpeed = 0.1f;
            await _follower.DOMove(originPos, moveSpeed).WithCancellation(_token.Token);

            // ���B�_�Ɉړ�
            moveSpeed = _moveSpeedOrigin;
            await _follower.DOMove(upMove, moveSpeed).WithCancellation(_token.Token);

            // ���B�_�ɂ����Ƃ�
            await _follower.DOMove(_movePos, moveSpeed).WithCancellation(_token.Token);
            _followerController.IsAction = false;
        }
    }
}
