using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;


/// <summary>
/// �f�b�h�]�[���ɂ���ʂ̕ω���Z�߂��N���X
/// </summary>
public class DeadZoneManager : MonoBehaviour
{
    // �f�b�h�]�[�����������Ƃ��ɉ�ʂ�Ԃ����鏈��
    public Volume _postProcessVolume; // �|�X�g�v���Z�X�{�����[��
    public float _warningDistance = 30.0f; // �x������
    public Transform _player; // �v���C���[��Transform
    private UnityEngine.Rendering.Universal.Vignette _vignette; // Vignette�G�t�F�N�g
    [SerializeField] float _maxIntensity = 0.5f; // Vignette�G�t�F�N�g�̍ő�l

   
    public AudioSource audioSource; // �S���̉���炷�̂ɕK�v

    // �v���C���[�ƃf�b�h�]�[�����G�ꂽ�ۂɉ�ʂ��O���[�ɂ��鏈��
    private ColorGrading colorCurves;
    private ColorCurves _colorCurves;

    private void Start()
    {
        // �|�X�g�v���Z�X�{�����[������Vignette�G�t�F�N�g���擾
        if (_postProcessVolume.profile.TryGet<UnityEngine.Rendering.Universal.Vignette>(out var vignette))
        {
            this._vignette = vignette;

        }
        // �J���[�J�[�u���擾
        if (_postProcessVolume.profile.TryGet<ColorCurves>(out var colorCurves))
        {
            this._colorCurves = colorCurves;
            colorCurves.active = false;
        }
        
    }

    private void Update()
    {
        // �v���C���[�ƃf�b�h�]�[���Ƃ̋������v�Z
        float distance = Vector3.Distance(transform.position, _player.position);

        // �f�b�h�]�[���Ƃ̋�����WarningDistance�Ȃ�Vignette�G�t�F�N�g��L���ɂ���
        if (distance <= _warningDistance)
        {
            _vignette.active = true;
            _vignette.color.Override(Color.red);
            // �f�b�h�]�[���Ƃ̋����ɉ����ĉ�ʂ̐Ԃ�Z������
            _vignette.intensity.value = Mathf.Clamp(20f / distance, 0f, 1.0f) * _maxIntensity;


            // �S���̉������ɍĐ�����Ă��Ȃ��ꍇ�ɂ͉����Đ�����
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            // �f�b�h�]�[���Ƃ̋����ɉ����ĐS���̉��̃{�����[���𒲐�
            audioSource.volume = 1.0f - Mathf.Clamp01(distance / _warningDistance);
            // �����ɉ����ĉ��̃s�b�`�𑁂߂�
            float pitchMultiplier = 1.0f + Mathf.Clamp01((_warningDistance - distance) / _warningDistance);
            audioSource.pitch = Mathf.Clamp(pitchMultiplier, 1.0f, 1.5f);
        }
        else
        {
            // �f�b�h�]�[�����v���C���[���牓����������Vignette�G�t�F�N�g�𖳌��ɂ���
            _vignette.active = false;

            // �����P�b��ɒ�~����(Dotween)
            audioSource.DOFade(endValue: 0f, duration: 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(_colorCurves != null)
            {
                _colorCurves.active = true;
            }
          
        }
    }
}


