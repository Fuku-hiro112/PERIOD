using UnityEngine;
using Character;

/// <Summary>
/// エンジニアがプレイヤーを追従する処理　参考元：https://nekojara.city/unity-smooth-damp
/// </Summary>
public class FollowerController : MonoBehaviour
{
    // ターゲット（メインキャラクター）
    [SerializeField] private GameObject _operator;

    // 追従させるオブジェクト (追従キャラクター)
    [SerializeField] private GameObject _follower;

    // 目標値に到達するまでのおおよその時間
    [SerializeField] private float _smoothTime;

    // 最高速度
    [SerializeField] private float _maxSpeed = 10.0f;

    // メインキャラクターとの一定距離を維持
    [SerializeField] private float _distanceFromOperator;

    private Vector3 _direction;

    private bool _isFollow = true;

    // FollowerMoveをインスタンス
    FollowerMove _followerMove;

    CharacterTurnAround _characterTurn = new CharacterTurnAround();
    CharacterClimb _characterClimb = new CharacterClimb();

    private void Awake()
    {
        _followerMove = new FollowerMove(_distanceFromOperator, _smoothTime, _maxSpeed);
    }

    private void Update()
    {
        if (_isFollow)
        {
            _followerMove.MoveFollower();
            _direction = _characterTurn.MyTargetDirection();
            _characterTurn.TurnAround(_direction);
            _characterClimb.Climb(_direction);
        }
    }

    /// <summary>
    /// キャラクター情報の更新
    /// </summary>
    /// <param name="player"></param>
    /// <param name="follower"></param>
    public void CharacterChange(GameObject player, GameObject follower)
    {
        _operator = player;
        _follower = follower;
        _followerMove.InFolloewrCharacter(_operator, _follower);
        _characterTurn.InFolloewrCharacter(_operator, _follower);
        _characterClimb.InCharacter(_follower);
    }
}












