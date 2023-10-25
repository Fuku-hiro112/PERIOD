using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class GardenGenerator : MonoBehaviour
{
    private GameObject[] _gardenKinds;
    [SerializeField]private GameObject _prepareGarden;
    Vector3 _gardenPos = Vector3.zero;
    private int _mapCount = 0;

    void Start()
    {
        int childNum = transform.childCount;// 子オブジェクトの数
        
        // 箱庭の種類を格納
        _gardenKinds = new GameObject[childNum];
        for (int i = 0; i < childNum; i++)
        {
            _gardenKinds[i] = transform.GetChild(i).gameObject;

            // 位置情報の初期化
            _gardenKinds[i].transform.position = Vector3.zero;
        }

        // マップの箱庭を決定
        _mapCount++;
        int gardenSetNum = _mapCount + 1;// 何個の箱庭を生成するか +1は準備の箱庭　
        GameObject[] gardenSet = new GameObject[gardenSetNum];
        for (int i = 0; i < _mapCount; i++)// _mapCount分ループする
        {
            int SetIndex = Random.Range(0,childNum);
            gardenSet[i] = _gardenKinds[SetIndex];
        }
        // 最後に箱庭を追加
        gardenSet[gardenSetNum - 1] = _prepareGarden;

        // 箱庭生成
        for (int i = 0; i < gardenSet.Length; i++)
        {
            float halfScalZ = gardenSet[i].transform.localScale.z * 10 /2;// 箱庭の奥行の半分長さを取る メッシュによって変わるので変更が必要
            _gardenPos += new Vector3(0,0,halfScalZ);
            Instantiate(gardenSet[i], _gardenPos, Quaternion.Euler(Vector3.zero));// i番目の箱庭を生成　回転はゼロ
        }
    }

    void Update()
    {
        
    }

}
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
