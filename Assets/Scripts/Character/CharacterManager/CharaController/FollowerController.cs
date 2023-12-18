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

    [SerializeField] private bool _isFollow = true;
    public bool IsAction = false;

    // FollowerMoveをインスタンス
    FollowerMove _followerMove;

    CharacterTurnAround _characterTurnAround = new CharacterTurnAround();
    CharacterClimb _characterClimb = new CharacterClimb();

    public CharacterTurnAround CharacterTurnAround { get => _characterTurnAround; private set => _characterTurnAround = value; }
    public bool IsFollow { get => _isFollow; private set => _isFollow = value; }

    public void Follow(bool isFollow)
    {
        IsFollow = isFollow;
    }

    private void Awake()
    {
        _followerMove = new FollowerMove(_distanceFromOperator, _smoothTime, _maxSpeed);
    }

    private void Update()
    {
        if (IsFollow && !IsAction)
        {
            _followerMove.MoveFollower();
            _direction = CharacterTurnAround.MyTargetDirection();
            CharacterTurnAround.TurnAround(_direction);
            //_characterClimb.Climb(_direction); // 不必要
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
        CharacterTurnAround.InFolloewrCharacter(_operator, _follower);
        _characterClimb.InCharacter(_follower);
    }
}












