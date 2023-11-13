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
        [SerializeField]// �J�n�n�_
        private Transform _startTransform;
        [SerializeField] // �I���n�_
        private Transform _endTransform;
        [SerializeField]// �`�F�b�N�|�C���g�z��
        private Transform[] _checkPointTransform;
        */
    }
}

