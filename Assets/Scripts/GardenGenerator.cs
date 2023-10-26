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
    [SerializeField] int _daysUpWave = 5;// wave���オ�����
    private int _dayNum = 1;
    private int _waveNum = 1;
    int idx = 0;

    void Start()
    {
        int childNum = transform.childCount;// �q�I�u�W�F�N�g�̐�
        
        // ����̎�ނ��i�[
        _gardenKinds = SetGardenKinds(childNum);



        // �o�����锠�������
        List<GameObject> gardenSet = SetRandomGarden(childNum);



        /*
        // ���됶��
        for (int i = 0; i < gardenSet.Length; i++)
        {
            float halfScalZ = gardenSet[i].transform.localScale.z * 10 /2;// ����̉��s�̔������������ ���b�V���ɂ���ĕς��̂ŕύX���K�v
            _gardenPos += new Vector3(0,0,halfScalZ);
            Instantiate(gardenSet[i], _gardenPos, Quaternion.Euler(Vector3.zero));// i�Ԗڂ̔���𐶐��@��]�̓[��
        }*/
        /*
        // ���됶��
        while (idx < gardenSet.Length)// 
        {
            while (_dayNum % _daysUpWave == 0)// _dayUpWave���Ƃ�
            {
                _waveNum++;
            }
            _dayNum++;
            GenerateGarden(gardenSet);
        }*/
    }

    /// <summary>
    /// ����̎�ނ��i�[����
    /// </summary>
    /// <param name="childNum"></param>
    /// <returns>����̎�ނ��i�[���郊�X�g</returns>
    private GameObject[] SetGardenKinds(int childNum)
    {
        // ����̎�ނ��i�[
        GameObject[] gardenKinds = new GameObject[childNum];
        for (int i = 0; i < childNum; i++)
        {
            gardenKinds[i] = transform.GetChild(i).gameObject;

            // �ʒu���̏�����
            gardenKinds[i].transform.position = Vector3.zero;
        }
        return gardenKinds;
    }

    /// <summary>
    /// �o�����锠����Z�b�g����
    /// </summary>
    /// <param name="childNum"></param>
    /// <returns>���肳�ꂽ����̔z��</returns>
    private List<GameObject> SetRandomGarden(int childNum)
    {
        // �}�b�v�̔��������
        int gardenSetNum = _waveNum + 1;// ���̔���𐶐����邩 +1�͏����̔���
        List<GameObject> gardens = new List<GameObject>();
        // wave�̌���
        for (int i = 1; i < _waveNum; i++)// _waveNum�����[�v����
        {
            int index = UnityEngine.Random.Range(0, childNum);//  using:System�ƞB������������UnityEngine��t���Ă��܂�
            gardens[i] = _gardenKinds[index];
        }

        // �Ō�ɔ����ǉ�
        gardens[gardenSetNum - 1] = _prepareGarden;

        return gardens;
    }
    private Vector3 SetGardenPos(GameObject[] gardenSet)
    {


        return new Vector3(0,0,0);
    }
    /*
    /// <summary>
    /// ���됶���Ń}�b�v�����
    /// </summary>
    /// <param name="gardenSet"></param>
    private async UniTaskVoid GenerateGarden(GameObject[] gardenSet)
    {
        float gardenHalfScalZ = gardenSet[idx].transform.localScale.z * 10 / 2;// ����̉��s�̔������������ ���b�V���ɂ���ĕς��̂ŕύX���K�v
        // ���됶��
        Instantiate(gardenSet[idx], _gardenPos, Quaternion.Euler(Vector3.zero));// i�Ԗڂ̔���𐶐��@��]�̓[��
        // await UniTask.WaitUntil(()=> );// prepereGarden�̏ꏊ�܂œ��B������J�n
        // ���̔���̈ʒu���Z�o
        float nextGardenHalfScalZ = 0;

        try
        {
            nextGardenHalfScalZ = gardenSet[++idx].transform.localScale.z * 10 / 2;
        }
        catch (IndexOutOfRangeException)
        {
            _gardenPos += new Vector3(0, 0, gardenHalfScalZ);// �ʒu�̌���
            return;
        }

        float gardenPosZ = gardenHalfScalZ + nextGardenHalfScalZ;// ���v
        _gardenPos += new Vector3(0, 0, gardenPosZ);// �ʒu�̌���
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
