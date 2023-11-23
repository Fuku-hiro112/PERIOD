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
    [SerializeField]private int _daysUpWave = 4;// wave���オ�����
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

        int childNum = transform.childCount;// �q�I�u�W�F�N�g�̐�
        // ����̎�ނ��i�[
        _gardenKinds = SetGardenKinds(childNum);

        ControlGardenAsync(childNum, token).Forget();

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
    /// ������o��������
    /// </summary>
    /// <param name="childNum"></param>
    /// <returns></returns>
    private async UniTaskVoid ControlGardenAsync(int childNum, CancellationToken token)
    {
        while (true)
        {
            // �o�����锠�������
            GameObject[] gardens = SetRandomGarden(childNum);
            // ���됶��
            GenerateGarden(gardens);
            await UniTask.WaitUntil(()=> _player.position.z >= _gardenPos.z, cancellationToken:token);
            // day�ɂ����wave�𑝂₷
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
    /// ���됶��
    /// </summary>
    /// <param name="gardens"></param>
    private void GenerateGarden(GameObject[] gardens)
    {
        foreach (GameObject garden in gardens)
        {
            // �o���ʒu���X�V
            float gardenScalZ = garden.transform.localScale.z * 10;// mesh�̑傫���ɂ���ĕς��̂ŕύX���K�v
            _gardenPos += new Vector3(0, 0, gardenScalZ);// ����̃X�P�[��������

            // ���됶��
            Instantiate(garden,�@_gardenPos,�@Quaternion.Euler(Vector3.zero));
        }
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
    private GameObject[] SetRandomGarden(int childNum)
    {
        GameObject[] gardens = new GameObject[_waveNum+1];// wave + ��������
        for (int i = 0; i < _waveNum; i++)// _waveNum�����[�v����
        {
            int index = UnityEngine.Random.Range(0, childNum);//  using:System�ƞB������������UnityEngine��t���Ă��܂�
            gardens[i] = _gardenKinds[index];
        }

        // �Ō�ɔ����ǉ�
        gardens[_waveNum] = _prepareGarden;

        return gardens;
    }
}