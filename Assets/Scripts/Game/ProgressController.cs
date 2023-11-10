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
        // 開始時日数とウェーブ
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
        /// 日付を増やす
        /// </summary>
        public void IncreaseDay()
        {
            _day++;
        }
        /// <summary>
        /// ウェーブと日付を増やす
        /// </summary>
        public void IncreaseProgress()
        {
            // ウェーブが上がる日数に到達したら
            if (_day % DaysUpWave == 0)
            {
                _wave++;
            }
            IncreaseDay();
        }

        //TODO: viewに移動予定
        /// <summary>
        /// day・waveテキスト表示
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
