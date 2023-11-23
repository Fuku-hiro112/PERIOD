using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garden
{
    public class GardenSetter
    {
        // 箱庭情報
        private GameObject[] _gardenKinds;
        private GameObject _prepareGarden;

        public GardenSetter(GardenMap gardenMap)
        {
            _gardenKinds   = gardenMap.Gardens;
            _prepareGarden = gardenMap.Prepare;
        }

        /// <summary>
        /// 出現する箱庭をセットする
        /// </summary>
        /// <param name="childNum"></param>
        /// <returns>決定された箱庭の配列</returns>
        public GameObject[] SetGarden(GameMode mode, int maxLangth, int minLangth = 1/*最低でも長さは１*/)
        {
            switch (mode)
            {
                // WAVEを重ねるごとに箱庭の連結数の増加と高難易度のギミックが出現するようになり、段階的にクリアの難易度が上がっていくゲームモード。
                case GameMode.Survival:
                    return Set(maxLangth);
                // WAVEの進行状況に関係なく、箱庭の連結数と高難易度のギミックがランダムで生成され続けるゲームモード。
                case GameMode.Cycle:
                    return SetRandom(maxLangth, minLangth);
                
                default:
                    Debug.Log("<color=red>存在しないゲームモードが渡されました。</color>");
                    // TODO: GameMode選択画面へ
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
            GameObject[] gardens = new GameObject[waveNum + 1];// wave + 準備箱庭
            for (int i = 0; i < waveNum; i++)// _waveNum分ループする
            {
                // TODO: waveが上がるごとにRangeの値が変化する
                int index = Random.Range(0, _gardenKinds.Length);// sysytemと曖昧なので明示している
                gardens[i] = _gardenKinds[index];
            }

            // 最後に箱庭を追加
            gardens[waveNum] = _prepareGarden;

            return gardens;
        }

        private GameObject[] SetRandom(int maxLange, int minLange)
        {
            // 箱庭の長さをランダムに決める
            int length = Random.Range(minLange,maxLange+1);// 1～maxlengeまで

            GameObject[] gardens = new GameObject[length + 1];// ランダムな長さ + 準備箱庭
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
            GameObject[] gardens = new GameObject[length + 1];// ランダムな長さ + 準備箱庭
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
            // 箱庭の長さをランダムに決める
            int length = Random.Range(1,maxLange+1);// 1～maxlengeまで
            return SetGarden(length);
        }*/
