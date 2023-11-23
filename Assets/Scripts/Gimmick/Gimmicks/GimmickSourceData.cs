using UnityEngine;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "GimmickSourceData", menuName = "ScriptableObject/Gimmick")]
    public class GimmickSourceData : ScriptableObject
    {
        [SerializeField]// ギミックのID
        private int _id;
        [SerializeField]// 開始地点
        private RectTransform _start;
        
        [SerializeField]// ギミックのプレファブ
        public GameObject Prefab;

        public int ID { get => _id; }
        public Vector3 StartPos { get => _start.position; }
    }
}

