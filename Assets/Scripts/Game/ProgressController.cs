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
        // 開始時 日数とウェーブ
        [SerializeField] 
        private Progres _progres;
        
        public int DaysUpWave { private get; set; } = 4;
        public Progres Progres { get => _progres; private set => _progres = value; }

        /// <summary>
        /// 日付を増やす
        /// </summary>
        public void IncreaseDay()
        {
            Progres.Day++;
        }
        /// <summary>
        /// ウェーブと日付を増やす
        /// </summary>
        public int IncreaseProgress()
        {
            // ウェーブが上がる日数に到達したら
            if (Progres.Day % DaysUpWave == 0)
            {
                Progres.Wave++;
            }
            IncreaseDay();
            return Progres.Wave;
        }

        //TODO: viewに移動予定
        /// <summary>
        /// day・waveテキスト表示
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
    // 基本的には1から始まるので変更しなくてよい
    public int Wave = 1;
    public int Day = 1;
}
