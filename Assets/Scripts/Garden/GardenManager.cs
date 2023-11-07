using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Garden
{
    public class GardenManager : MonoBehaviour
    {
        private GameObject[] _gardenKinds;
        [SerializeField] private GameObject _prepareGarden;
        private Vector3 _gardenPos = Vector3.zero;
        
        int _gardenNum = 5;
        GameMode _mode = GameMode.Cycle;

        [SerializeField] private int _daysUoWave = 4;
        private int _dayNum = 1;
        private int _waveNum = 1;
        private Transform _player;

        // Scripts
        private GardenGenerater _generater;
        private GardenSetter _setter;

        public void Start()
        {
            GameObject[] gardenKinds;
            GameObject   prepareGarden;

            GardenMap gardenMap
                       = new GardenMap(_gardenKinds, _prepareGarden);
            _generater = new GardenGenerater();
            _setter    = new GardenSetter(gardenMap);
        }
        private async UniTaskVoid ControlGardenAsync(CancellationToken token)
        {
            while (true)
            {
                GameObject[] garden = _setter.SetGarden(_mode, _waveNum, _gardenNum);
                _generater.GenerateGarden(garden, _gardenPos);
                await UniTask.WaitUntil(() => _player.position.z >= _gardenPos.z, cancellationToken: token);

                // ������wave�ǉ�
                // day��ǉ�
            }
        }
    }



    // 1��������̔��� ��������ȊO�ɂ��ǉ����������ꍇ�A�璷�ɂȂ邽�ߍ쐬
    public struct GardenMap
    {
        public GardenMap(GameObject[] gardens, GameObject prepare)
        {
            Gardens = gardens;
            Prepare = prepare;
        }
        public GameObject[] Gardens { get; private set; }
        public GameObject   Prepare { get; private set; }
    }
}
