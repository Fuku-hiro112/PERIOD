using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using System.Threading;
using UnityEngine.UI;

public class GardenGenerator : MonoBehaviour
{
    private GameObject[] _gardenKinds;
    [SerializeField]private GameObject _prepareGarden;
    Vector3 _gardenPos = Vector3.zero;
    [SerializeField]private int _daysUpWave = 4;// waveが上がる日数
    [SerializeField] private Text _dayText;
    [SerializeField] private Text _waveText;
    private int _dayNum = 1;
    private int _waveNum = 1;
    Transform _player;

    void Start()
    {
        //_dayText.text = $"{_dayNum}DAY";
        //_waveText.text = $"{_waveNum}WAVE";
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        CancellationToken token = this.GetCancellationTokenOnDestroy();

        int childNum = transform.childCount;// 子オブジェクトの数
        // 箱庭の種類を格納
        _gardenKinds = SetGardenKinds(childNum);

        ControlGardenAsync(childNum, token).Forget();

        /*
        // 箱庭生成
        for (int i = 0; i < gardenSet.Length; i++)
        {
            float halfScalZ = gardenSet[i].transform.localScale.z * 10 /2;// 箱庭の奥行の半分長さを取る メッシュによって変わるので変更が必要
            _gardenPos += new Vector3(0,0,halfScalZ);
            Instantiate(gardenSet[i], _gardenPos, Quaternion.Euler(Vector3.zero));// i番目の箱庭を生成　回転はゼロ
        }*/
        /*
        // 箱庭生成
        while (idx < gardenSet.Length)// 
        {
            while (_dayNum % _daysUpWave == 0)// _dayUpWaveごとに
            {
                _waveNum++;
            }
            _dayNum++;
            GenerateGarden(gardenSet);
        }*/
    }
    /// <summary>
    /// 箱庭を出現させる
    /// </summary>
    /// <param name="childNum"></param>
    /// <returns></returns>
    private async UniTaskVoid ControlGardenAsync(int childNum, CancellationToken token)
    {
        while (true)
        {
            // 出現する箱庭を決定
            GameObject[] gardens = SetRandomGarden(childNum);
            // 箱庭生成
            GenerateGarden(gardens);
            await UniTask.WaitUntil(()=> _player.position.z >= _gardenPos.z, cancellationToken:token);
            // dayによってwaveを増やす
            if (_dayNum % _daysUpWave == 0) 
            { 
                _waveNum++;
                //_waveText.text = $"{_waveNum}WAVE";
            }
            _dayNum++;
            //_dayText.text = $"{_dayNum}DAY";
        }
    }
    /// <summary>
    /// 箱庭生成
    /// </summary>
    /// <param name="gardens"></param>
    private void GenerateGarden(GameObject[] gardens)
    {
        foreach (GameObject garden in gardens)
        {
            // 出現位置を更新
            float gardenScalZ = garden.transform.localScale.z * 10;// meshの大きさによって変わるので変更が必要
            _gardenPos += new Vector3(0, 0, gardenScalZ);// 箱庭のスケール分足す

            // 箱庭生成
            Instantiate(garden,　_gardenPos,　Quaternion.Euler(Vector3.zero));
        }
    }

    /// <summary>
    /// 箱庭の種類を格納する
    /// </summary>
    /// <param name="childNum"></param>
    /// <returns>箱庭の種類を格納するリスト</returns>
    private GameObject[] SetGardenKinds(int childNum)
    {
        // 箱庭の種類を格納
        GameObject[] gardenKinds = new GameObject[childNum];
        for (int i = 0; i < childNum; i++)
        {
            gardenKinds[i] = transform.GetChild(i).gameObject;

            // 位置情報の初期化
            gardenKinds[i].transform.position = Vector3.zero;
        }
        return gardenKinds;
    }

    /// <summary>
    /// 出現する箱庭をセットする
    /// </summary>
    /// <param name="childNum"></param>
    /// <returns>決定された箱庭の配列</returns>
    private GameObject[] SetRandomGarden(int childNum)
    {
        GameObject[] gardens = new GameObject[_waveNum+1];// wave + 準備箱庭
        for (int i = 0; i < _waveNum; i++)// _waveNum分ループする
        {
            int index = UnityEngine.Random.Range(0, childNum);//  using:Systemと曖昧だったためUnityEngineを付けています
            gardens[i] = _gardenKinds[index];
        }

        // 最後に箱庭を追加
        gardens[_waveNum] = _prepareGarden;

        return gardens;
    }
}