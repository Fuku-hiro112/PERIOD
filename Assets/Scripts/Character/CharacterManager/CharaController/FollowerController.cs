using UnityEngine;
using Character;
using TMPro;
using Unity.VisualScripting;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    [SerializeField] private float _maxSpeed = 10.0f;

    // ���C���L�����N�^�[�Ƃ̈�苗�����ێ�
    [SerializeField] private float _distanceFromOperator;

    [SerializeField] private Vector3 _direction;

    [SerializeField] float _distance;

    [SerializeField] private bool _isFollow = true;
    public bool IsAction = false;

    // FollowerMove���C���X�^���X
    FollowerMove _followerMove;

    CharacterTurnAround _characterTurnAround = new CharacterTurnAround();
    CharacterClimb _characterClimb = new CharacterClimb();

    public CharacterTurnAround CharacterTurnAround { get => _characterTurnAround; private set => _characterTurnAround = value; }
    public bool IsFollow { get => _isFollow; private set => _isFollow = value; }

    // ��ɍ����ւ�
    [SerializeField] Animator _boyAnimator;
    [SerializeField] Animator _engineerAnimator;

    [SerializeField] Animator _playerAnimator;

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

            //_characterClimb.Climb(_direction);
            float distance = Vector3.Distance(_operator.transform.position, _follower.transform.position);
            _distance = distance - _distanceFromOperator;
            float speed;
            if(_distance > 0.3)
            {
                speed = 1f;
            }
            else if(_distance > 0.1f)
            {
                speed = 0.5f;
            }
            else
            {
                speed = 0;
            }
            _playerAnimator.SetFloat("Speed", speed);
          
        }
        
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
        CharacterTurnAround.InFolloewrCharacter(_operator, _follower);
        _characterClimb.InCharacter(_follower);
    }
}












