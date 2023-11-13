using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [Serializable]
    public class ProgressController
    {
        // �J�n�� �����ƃE�F�[�u
        [SerializeField] 
        private Progres _progres;
        
        public int DaysUpWave { private get; set; } = 4;
        public Progres Progres { get => _progres; private set => _progres = value; }

        /// <summary>
        /// ���t�𑝂₷
        /// </summary>
        public void IncreaseDay()
        {
            Progres.Day++;
        }
        /// <summary>
        /// �E�F�[�u�Ɠ��t�𑝂₷
        /// </summary>
        public int IncreaseProgress()
        {
            // �E�F�[�u���オ������ɓ��B������
            if (Progres.Day % DaysUpWave == 0)
            {
                Progres.Wave++;
            }
            IncreaseDay();
            return Progres.Wave;
        }

        //TODO: view�Ɉړ��\��
        /// <summary>
        /// day�Ewave�e�L�X�g�\��
        /// </summary>
        private Text _dayText;
        private Text _waveText;

        public void OnUpdateText()
        {
            _dayText.text = $"{Progres.Day}DAY";
            _waveText.text = $"{Progres.Wave}WAVE";
        }
    }
}
[Serializable]
public class Progres
{
    // ��{�I�ɂ�1����n�܂�̂ŕύX���Ȃ��Ă悢
    public int Wave = 1;
    public int Day = 1;
}
