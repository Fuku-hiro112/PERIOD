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

namespace Garden
{
    public class GardenGenerator : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _prepareGarden;
        [SerializeField] 
        private Text _dayText;
        [SerializeField] 
        private Text _waveText;
        [SerializeField]// 初期高さ　Playerをy=0にしたいため
        Vector3 _gardenPos = new Vector3(0, 9.58f, 0);
        [SerializeField] 
        private int _daysUpWave = 4;// waveが上がる日数

        private GameObject[] _gardenKinds;
        private Transform _player;
        private int _waveNum = 1;
        private int _dayNum = 1;

        //HACK: データベースorオブジェクトプールによる処理負荷軽減を推奨
        public GameObject[] GardenKinds { get => _gardenKinds; }
        public GameObject PrepareGarden { get => _prepareGarden; }

        void Start()
        {
            _dayText.text = $"{_dayNum}DAY";
            _waveText.text = $"{_waveNum}WAVE";
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            CancellationToken token = this.GetCancellationTokenOnDestroy();

            int childNum = transform.childCount;// 子オブジェクトの数
                                                // 箱庭の種類を格納
            _gardenKinds = SetGardenKinds(childNum);

            ControlGardenAsync(childNum, token).Forget();
        }
        /// <summary>
        /// 箱庭を出現させる
        /// </summary>
        /// <param name="childNum"></param>
        /// <returns></returns>
        private async UniTaskVoid ControlGardenAsync(int childNum, CancellationToken token)
        {
            while (true)
            {
                // 出現する箱庭を決定
                GameObject[] gardens = SetRandomGarden(childNum);
                // 箱庭生成
                GenerateGarden(gardens);
                await UniTask.WaitUntil(() => _player.position.z >= _gardenPos.z, cancellationToken: token);
                // dayによってwaveを増やす
                if (_dayNum % _daysUpWave == 0)
                {
                    _waveNum++;
                    _waveText.text = $"{_waveNum}WAVE";
                }
                _dayNum++;
                _dayText.text = $"{_dayNum}DAY";
            }
        }
        /// <summary>
        /// 箱庭生成
        /// </summary>
        /// <param name="gardens"></param>
        private void GenerateGarden(GameObject[] gardens)
        {
            foreach (GameObject garden in gardens)
            {
                // 出現位置を更新
                Vector3 boundsSize = garden.GetComponent<MeshFilter>().mesh.bounds.size;
                float gardenSizeX = boundsSize.x * garden.transform.localScale.x;//
                _gardenPos += new Vector3(0, 0, gardenSizeX);// 箱庭のサイズ分足す

                // 箱庭生成
                Instantiate(garden, _gardenPos, Quaternion.Euler(0,90,0));
            }
        }

        /// <summary>
        /// 箱庭の種類を格納する
        /// </summary>
        /// <param name="childNum"></param>
        /// <returns>箱庭の種類を格納するリスト</returns>
        private GameObject[] SetGardenKinds(int childNum)
        {
            // 箱庭の種類を格納
            GameObject[] gardenKinds = new GameObject[childNum];
            for (int i = 0; i < childNum; i++)
            {
                gardenKinds[i] = transform.GetChild(i).gameObject;

                // 位置情報の初期化
                gardenKinds[i].transform.position = Vector3.zero;
            }
            return gardenKinds;
        }

        /// <summary>
        /// 出現する箱庭をセットする
        /// </summary>
        /// <param name="childNum"></param>
        /// <returns>決定された箱庭の配列</returns>
        private GameObject[] SetRandomGarden(int childNum)
        {
            GameObject[] gardens = new GameObject[_waveNum + 1];// wave + 準備箱庭
            for (int i = 0; i < _waveNum; i++)// _waveNum分ループする
            {
                int index = UnityEngine.Random.Range(0, childNum);//  using:Systemと曖昧だったためUnityEngineを付けています
                gardens[i] = _gardenKinds[index];
            }

            // 最後に箱庭を追加
            gardens[_waveNum] = _prepareGarden;

            return gardens;
        }
    }
}