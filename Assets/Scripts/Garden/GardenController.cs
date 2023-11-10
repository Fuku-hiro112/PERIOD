using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Garden
{
    //[Serializable]
    public class GardenController : IGardenController
    {
        // static�������Ă��Ă���@��
        [SerializeField] private GameObject[] _gardenKinds ;
        [SerializeField] private GameObject _prepareGarden ;
        private Vector3 _gardenPos = Vector3.zero;
        
        int _gardenNum = 5;
        GameMode _mode = GameMode.Cycle;

        [SerializeField] private int _daysUoWave = 4;
        private int _dayNum = 1;
        private int _waveNum = 1;
        [SerializeField] private Transform _player;

        // Scripts
        [SerializeField]
        private GardenGenerater _generater;
        [SerializeField]
        private GardenSetter _setter;

        public void OnStart()
        {
            // DB���玝���Ă���
            GardenGenerator _g = GameObject.FindAnyObjectByType<GardenGenerator>();//TODO�F�f�[�^�x�[�X�����������X�����ĉ�����
            GameObject[] gardenKinds = _g._gardenKinds;
            GameObject   prepareGarden = _g._prepareGarden;
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            GardenMap gardenMap
                       = new GardenMap(gardenKinds, prepareGarden);
            _generater = new GardenGenerater();
            _setter    = new GardenSetter(gardenMap);
            
        }
        /// <summary>
        /// �v���C���[�̈ʒu�ɂ���Ĕ���𐶐�����A
        /// </summary>
        /// <param name="action"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async UniTaskVoid ControlGardenAsync(Action action, GameMode mode, CancellationToken token)
        {
            while (true)
            {
                GameObject[] garden = _setter.SetGarden(mode, _waveNum, _gardenNum);// �p���ǂ��K�v
                _gardenPos = _generater.GenerateGarden(garden, _gardenPos);
                await UniTask.WaitUntil(() => _player.position.z >= _gardenPos.z, cancellationToken: token);

                // ���t�Awave�𑝂₷
                action?.Invoke();
            }
        }
        //TODO: ����̕\����\���Ǘ�
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
