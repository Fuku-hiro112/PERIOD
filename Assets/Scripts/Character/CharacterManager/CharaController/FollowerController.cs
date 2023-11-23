using UnityEngine;
using Character;

/// <Summary>
/// �G���W�j�A���v���C���[��Ǐ]���鏈���@�Q�l���Fhttps://nekojara.city/unity-smooth-damp
/// </Summary>
public class FollowerController : MonoBehaviour
{
    // �^�[�Q�b�g�i���C���L�����N�^�[�j
    [SerializeField] private GameObject _operator;

    // �Ǐ]������I�u�W�F�N�g (�Ǐ]�L�����N�^�[)
    [SerializeField] private GameObject _follower;

    // �ڕW�l�ɓ��B����܂ł̂����悻�̎���
    [SerializeField] private float _smoothTime;

    // �ō����x
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;

    // ���C���L�����N�^�[�Ƃ̋���
    [SerializeField] private float _distanceFromOperator;

    private Vector3 _direction;

    // FollowerMove���C���X�^���X
    FollowerMove _followerMove = new FollowerMove(2f, 0.1f, float.PositiveInfinity);

    CharacterTurnAround _characterTurn = new CharacterTurnAround();
    CharacterClimb _characterClimb = new CharacterClimb();

    private void Update()
    {
        _followerMove.MoveFollower();
        _direction = _characterTurn.MyTargetDirection();
        _characterTurn.TurnAround(_direction);
        _characterClimb.Climb(_direction);
    }

    /// <summary>
    /// �L�����N�^�[���̍X�V
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












