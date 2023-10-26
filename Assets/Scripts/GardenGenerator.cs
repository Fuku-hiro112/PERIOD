using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System;

public class GardenGenerator : MonoBehaviour
{
    private GameObject[] _gardenKinds;
    [SerializeField]private GameObject _prepareGarden;
    Vector3 _gardenPos = Vector3.zero;
    [SerializeField] int _daysUpWave = 5;// waveが上がる日数
    private int _dayNum = 1;
    private int _waveNum = 1;
    int idx = 0;

    void Start()
    {
        int childNum = transform.childCount;// 子オブジェクトの数
        
        // 箱庭の種類を格納
        _gardenKinds = SetGardenKinds(childNum);



        // 出現する箱庭を決定
        List<GameObject> gardenSet = SetRandomGarden(childNum);



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
    private List<GameObject> SetRandomGarden(int childNum)
    {
        // マップの箱庭を決定
        int gardenSetNum = _waveNum + 1;// 何個の箱庭を生成するか +1は準備の箱庭
        List<GameObject> gardens = new List<GameObject>();
        // waveの個数分
        for (int i = 1; i < _waveNum; i++)// _waveNum分ループする
        {
            int index = UnityEngine.Random.Range(0, childNum);//  using:Systemと曖昧だったためUnityEngineを付けています
            gardens[i] = _gardenKinds[index];
        }

        // 最後に箱庭を追加
        gardens[gardenSetNum - 1] = _prepareGarden;

        return gardens;
    }
    private Vector3 SetGardenPos(GameObject[] gardenSet)
    {


        return new Vector3(0,0,0);
    }
    /*
    /// <summary>
    /// 箱庭生成でマップを作る
    /// </summary>
    /// <param name="gardenSet"></param>
    private async UniTaskVoid GenerateGarden(GameObject[] gardenSet)
    {
        float gardenHalfScalZ = gardenSet[idx].transform.localScale.z * 10 / 2;// 箱庭の奥行の半分長さを取る メッシュによって変わるので変更が必要
        // 箱庭生成
        Instantiate(gardenSet[idx], _gardenPos, Quaternion.Euler(Vector3.zero));// i番目の箱庭を生成　回転はゼロ
        // await UniTask.WaitUntil(()=> );// prepereGardenの場所まで到達したら開始
        // 次の箱庭の位置を算出
        float nextGardenHalfScalZ = 0;

        try
        {
            nextGardenHalfScalZ = gardenSet[++idx].transform.localScale.z * 10 / 2;
        }
        catch (IndexOutOfRangeException)
        {
            _gardenPos += new Vector3(0, 0, gardenHalfScalZ);// 位置の決定
            return;
        }

        float gardenPosZ = gardenHalfScalZ + nextGardenHalfScalZ;// 合計
        _gardenPos += new Vector3(0, 0, gardenPosZ);// 位置の決定
    }
    */
    void WaveCount()
    {

    }
}
/*
public class Wave : MonoBehaviour
{

}
public class RandomGenerat : MonoBehaviour
{
    int generateNum = 1;
    Vector3 pos = Vector3.zero;
    RandomGenerat(GameObject[] _garden)
    {
        int index = Random.Range(0, _garden.Length);
        GameObject generateObj = _garden[index];
        Instantiate(generateObj, pos, Quaternion.identity);
        generateNum++;
    }
}

/*
// 生成手順

// 初回生成
int index = Random.Range(0, _garden.Length);
GameObject generateObj = _garden[index];
Instantiate(generateObj, pos, Quaternion.identity);

int generateNum = 2;
while ()
{
    int generateCount = 1;
    while (generateNum <= generateCount)
    {
        // 今回生成したオブジェクトの半分を足す
        float objScaleZ = generateObj.transform.localScale.z * 10;
        pos += new Vector3(0, 0, objScaleZ);

        index = Random.Range(0, _garden.Length);
        generateObj = _garden[index];

        objScaleZ = generateObj.transform.localScale.z * 10;
        pos += new Vector3(0, 0, objScaleZ);

        generateCount++;
    }
}



/*do
{
    // 生成するオブジェクトを決める
    index = Random.Range(0, _garden.Length);
    generateObj = _garden[index];

    // 生成
    Instantiate(generateObj, pos, Quaternion.identity);

    // 今回生成したオブジェクトの半分を足す
    float objScaleZ = generateObj.transform.localScale.z * 10;
    pos += new Vector3(0, 0, objScaleZ);
    // 次回生成したオブジェクトの半分を足す
    // 生成
}
while (_garden == null);

foreach (GameObject child in _garden)
{
    Instantiate(child, pos, Quaternion.identity);

    float objScaleZ = child.transform.localScale.z * 10;
    pos += new Vector3(0,0,objScaleZ);
}*/
