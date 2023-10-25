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
        int childNum = transform.childCount;// �q�I�u�W�F�N�g�̐�
        
        // ����̎�ނ��i�[
        _gardenKinds = new GameObject[childNum];
        for (int i = 0; i < childNum; i++)
        {
            _gardenKinds[i] = transform.GetChild(i).gameObject;

            // �ʒu���̏�����
            _gardenKinds[i].transform.position = Vector3.zero;
        }

        // �}�b�v�̔��������
        _mapCount++;
        int gardenSetNum = _mapCount + 1;// ���̔���𐶐����邩 +1�͏����̔���@
        GameObject[] gardenSet = new GameObject[gardenSetNum];
        for (int i = 0; i < _mapCount; i++)// _mapCount�����[�v����
        {
            int SetIndex = Random.Range(0,childNum);
            gardenSet[i] = _gardenKinds[SetIndex];
        }
        // �Ō�ɔ����ǉ�
        gardenSet[gardenSetNum - 1] = _prepareGarden;

        // ���됶��
        for (int i = 0; i < gardenSet.Length; i++)
        {
            float halfScalZ = gardenSet[i].transform.localScale.z * 10 /2;// ����̉��s�̔������������ ���b�V���ɂ���ĕς��̂ŕύX���K�v
            _gardenPos += new Vector3(0,0,halfScalZ);
            Instantiate(gardenSet[i], _gardenPos, Quaternion.Euler(Vector3.zero));// i�Ԗڂ̔���𐶐��@��]�̓[��
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
// �����菇

// ���񐶐�
int index = Random.Range(0, _garden.Length);
GameObject generateObj = _garden[index];
Instantiate(generateObj, pos, Quaternion.identity);

int generateNum = 2;
while ()
{
    int generateCount = 1;
    while (generateNum <= generateCount)
    {
        // ���񐶐������I�u�W�F�N�g�̔����𑫂�
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
    // ��������I�u�W�F�N�g�����߂�
    index = Random.Range(0, _garden.Length);
    generateObj = _garden[index];

    // ����
    Instantiate(generateObj, pos, Quaternion.identity);

    // ���񐶐������I�u�W�F�N�g�̔����𑫂�
    float objScaleZ = generateObj.transform.localScale.z * 10;
    pos += new Vector3(0, 0, objScaleZ);
    // ���񐶐������I�u�W�F�N�g�̔����𑫂�
    // ����
}
while (_garden == null);

foreach (GameObject child in _garden)
{
    Instantiate(child, pos, Quaternion.identity);

    float objScaleZ = child.transform.localScale.z * 10;
    pos += new Vector3(0,0,objScaleZ);
}*/
