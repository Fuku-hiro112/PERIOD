using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "GimmickSourceData", menuName = "ScriptableObject/Gimmick")]
    public class GimmickSourceDataBase : ScriptableObject
    {
        [SerializeField]// ギミックのID
        private int _id;
        [SerializeField]// 開始地点
        private RectTransform _start;

        [SerializeField]// ギミックのプレファブ
        public GameObject _prefab;//HACK: 命名規則後で変えます

        [NonSerialized]
        public GameObject Prefab;
        public int ID { get => _id; }
        public Vector3 StartPos { get => _start.position; }

        public virtual async UniTask HandleActionAsync(Collider other)
        {
            
        }
    } 
}

