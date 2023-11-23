using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garden
{
    public class GardenSetter
    {
        // ������
        private GameObject[] _gardenKinds;
        private GameObject _prepareGarden;

        public GardenSetter(GardenMap gardenMap)
        {
            _gardenKinds   = gardenMap.Gardens;
            _prepareGarden = gardenMap.Prepare;
        }

        /// <summary>
        /// �o�����锠����Z�b�g����
        /// </summary>
        /// <param name="childNum"></param>
        /// <returns>���肳�ꂽ����̔z��</returns>
        public GameObject[] SetGarden(GameMode mode, int maxLangth, int minLangth = 1/*�Œ�ł������͂P*/)
        {
            switch (mode)
            {
                // WAVE���d�˂邲�Ƃɔ���̘A�����̑����ƍ���Փx�̃M�~�b�N���o������悤�ɂȂ�A�i�K�I�ɃN���A�̓�Փx���オ���Ă����Q�[�����[�h�B
                case GameMode.Survival:
                    return Set(maxLangth);
                // WAVE�̐i�s�󋵂Ɋ֌W�Ȃ��A����̘A�����ƍ���Փx�̃M�~�b�N�������_���Ő������ꑱ����Q�[�����[�h�B
                case GameMode.Cycle:
                    return SetRandom(maxLangth, minLangth);
                
                default:
                    Debug.Log("<color=red>���݂��Ȃ��Q�[�����[�h���n����܂����B</color>");
                    // TODO: GameMode�I����ʂ�
                    return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="childNum"></param>
        /// <param name="waveNum"></param>
        /// <returns></returns>
        private GameObject[] Set(int waveNum)
        {
            GameObject[] gardens = new GameObject[waveNum + 1];// wave + ��������
            for (int i = 0; i < waveNum; i++)// _waveNum�����[�v����
            {
                // TODO: wave���オ�邲�Ƃ�Range�̒l���ω�����
                int index = Random.Range(0, _gardenKinds.Length);// sysytem�ƞB���Ȃ̂Ŗ������Ă���
                gardens[i] = _gardenKinds[index];
            }

            // �Ō�ɔ����ǉ�
            gardens[waveNum] = _prepareGarden;

            return gardens;
        }

        private GameObject[] SetRandom(int maxLange, int minLange)
        {
            // ����̒����������_���Ɍ��߂�
            int length = Random.Range(minLange,maxLange+1);// 1�`maxlenge�܂�

            GameObject[] gardens = new GameObject[length + 1];// �����_���Ȓ��� + ��������
            for (int i = 0; i < length; i++)
            {
                int index = Random.Range(0, _gardenKinds.Length);
                gardens[i] = _gardenKinds[index];
            }
            gardens[length] = _prepareGarden;

            return gardens;
        }
    }
}
/*private GameObject[] SetGarden(int length)
        {
            GameObject[] gardens = new GameObject[length + 1];// �����_���Ȓ��� + ��������
            for (int i = 0; i < length; i++)
            {
                int index = Random.Range(0, _gardenKinds.Length);
                gardens[i] = _gardenKinds[index];
            }
            gardens[length] = _prepareGarden;

            return gardens;
        }

        private GameObject[] SetRandomGarden(int minLange ,int maxLange)
        {
            // ����̒����������_���Ɍ��߂�
            int length = Random.Range(1,maxLange+1);// 1�`maxlenge�܂�
            return SetGarden(length);
        }*/
