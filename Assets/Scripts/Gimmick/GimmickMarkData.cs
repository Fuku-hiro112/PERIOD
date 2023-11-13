using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "Gimmick", menuName = "ScriptableObject/Gimmick")]
    public class GimmickMarkData : ScriptableObject
    {
        [SerializeField]
        public string Name;
        [SerializeField]
        public GameObject Prefab;
        /*
        [SerializeField]// 開始地点
        private Transform _startTransform;
        [SerializeField] // 終了地点
        private Transform _endTransform;
        [SerializeField]// チェックポイント配列
        private Transform[] _checkPointTransform;
        */
    }
}

