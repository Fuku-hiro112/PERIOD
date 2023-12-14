using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "GimmickSourceData", menuName = "ScriptableObject/Gimmick")]
    public class GimmickSourceDataBase : ScriptableObject
    {
        [SerializeField]// �M�~�b�N��ID
        private int _id;
        [SerializeField]// �J�n�n�_
        private RectTransform _start;

        [SerializeField]// �M�~�b�N�̃v���t�@�u
        public GameObject _prefab;//HACK: �����K����ŕς��܂�

        [NonSerialized]
        public GameObject Prefab;
        public int ID { get => _id; }
        public Vector3 StartPos { get => _start.position; }

        public virtual async UniTask HandleActionAsync(Collider other)
        {
            
        }
    } 
}

