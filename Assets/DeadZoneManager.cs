using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;


/// <summary>
/// デッドゾーンによる画面の変化を纏めたクラス
/// </summary>
public class DeadZoneManager : MonoBehaviour
{
    // デッドゾーンが迫ったときに画面を赤くする処理
    public Volume _postProcessVolume; // ポストプロセスボリューム
    public float _warningDistance = 30.0f; // 警告距離
    public Transform _player; // プレイヤーのTransform
    private UnityEngine.Rendering.Universal.Vignette _vignette; // Vignetteエフェクト
    [SerializeField] float _maxIntensity = 0.5f; // Vignetteエフェクトの最大値

   
    public AudioSource audioSource; // 心臓の音を鳴らすのに必要

    // プレイヤーとデッドゾーンが触れた際に画面をグレーにする処理
    private ColorGrading colorCurves;
    private ColorCurves _colorCurves;

    private void Start()
    {
        // ポストプロセスボリュームからVignetteエフェクトを取得
        if (_postProcessVolume.profile.TryGet<UnityEngine.Rendering.Universal.Vignette>(out var vignette))
        {
            this._vignette = vignette;

        }
        // カラーカーブを取得
        if (_postProcessVolume.profile.TryGet<ColorCurves>(out var colorCurves))
        {
            this._colorCurves = colorCurves;
            colorCurves.active = false;
        }
        
    }

    private void Update()
    {
        // プレイヤーとデッドゾーンとの距離を計算
        float distance = Vector3.Distance(transform.position, _player.position);

        // デッドゾーンとの距離がWarningDistanceならVignetteエフェクトを有効にする
        if (distance <= _warningDistance)
        {
            _vignette.active = true;
            _vignette.color.Override(Color.red);
            // デッドゾーンとの距離に応じて画面の赤を濃くする
            _vignette.intensity.value = Mathf.Clamp(20f / distance, 0f, 1.0f) * _maxIntensity;


            // 心臓の音が既に再生されていない場合には音を再生する
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            // デッドゾーンとの距離に応じて心臓の音のボリュームを調整
            audioSource.volume = 1.0f - Mathf.Clamp01(distance / _warningDistance);
            // 距離に応じて音のピッチを早める
            float pitchMultiplier = 1.0f + Mathf.Clamp01((_warningDistance - distance) / _warningDistance);
            audioSource.pitch = Mathf.Clamp(pitchMultiplier, 1.0f, 1.5f);
        }
        else
        {
            // デッドゾーンがプレイヤーから遠ざかったらVignetteエフェクトを無効にする
            _vignette.active = false;

            // 音を１秒後に停止する(Dotween)
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


