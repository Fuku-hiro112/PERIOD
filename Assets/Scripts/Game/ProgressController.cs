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
        // �J�n�������ƃE�F�[�u
        [SerializeField]
        private int _day = 1;
        [SerializeField]
        private int _wave = 1;

        public int DaysUpWave { private get; set; } = 4;
        //public int Day  => _day;
        //public int Wave => _wave;

        public ProgressController()
        {
            _day = 1;
            _wave = 1;
        }
        /// <summary>
        /// ���t�𑝂₷
        /// </summary>
        public void IncreaseDay()
        {
            _day++;
        }
        /// <summary>
        /// �E�F�[�u�Ɠ��t�𑝂₷
        /// </summary>
        public void IncreaseProgress()
        {
            // �E�F�[�u���オ������ɓ��B������
            if (_day % DaysUpWave == 0)
            {
                _wave++;
            }
            IncreaseDay();
        }

        //TODO: view�Ɉړ��\��
        /// <summary>
        /// day�Ewave�e�L�X�g�\��
        /// </summary>
        private Text _dayText;
        private Text _waveText;

        public void OnUpdateText()
        {
            _dayText.text = $"{_day}DAY";
            _waveText.text = $"{_wave}WAVE";
        }
    }
}
