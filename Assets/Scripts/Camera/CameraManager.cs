using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Character;
using Cysharp.Threading.Tasks;
using System.Threading;

public class CameraManager : MonoBehaviour
{
    CameraMove _move;
    Transform _transform;
    private IOperatorInput _input;
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _boy;
    [SerializeField] private Transform _engineer;
    [SerializeField] private bool _isShake = false;
    [SerializeField] private bool _isMove = false;
    [SerializeField] private bool _isChange = false;
    [SerializeField] private float _distance, _smoothTime, _maxSpeed;
    [SerializeField] private float _duration = 0.2f;
    [SerializeField] private float _magnitude = 0.3f;
    private CancellationToken _token;

    private void Start()
    {
        _input = CharacterManager.OperatorInput;
        _transform = GetComponent<Transform>();
        _move = new CameraMove(_transform, _distance, _smoothTime, _maxSpeed);
        _target = _boy;
        _move.TargetChange(_target);
        _token = this.GetCancellationTokenOnDestroy();
        _isMove = true;
    }

    private void Update()
    {
        if (_isShake)
        {
            Shake().Forget();
            _isShake = false;
        }

        if (_isMove)
        {
            _move.Follower();

        }

        TargetChange();

    }
    private void TargetChange()
    {
        if (_input.IsChange() || _isChange)
        {
            if (_target == _boy)
            {
                _target = _engineer;
                _move.TargetChange(_target);
            }
            else
            {
                _target = _boy;
                _move.TargetChange(_target);
            }
            _isChange = false;
        }
    }

    public async UniTask Shake()
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            transform.position = originalPosition + Random.insideUnitSphere * _magnitude;
            elapsed += Time.deltaTime;
            await UniTask.DelayFrame(1, cancellationToken: _token) ;
        }
        transform.position = originalPosition;
    }
}
