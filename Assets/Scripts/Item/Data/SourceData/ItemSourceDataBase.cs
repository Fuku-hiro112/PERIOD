using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class ItemSourceDataBase : ScriptableObject
    {
        [SerializeField]// �A�C�e����ID
        private int _id;
        [SerializeField]// �A�C�e����
        private string _name;
        [SerializeField]// �A�C�e���摜(�C���x���g���\����)
        private Sprite _sprite;

        public int ID       { get => _id; }
        public string Name  { get => _name; }
        public Sprite Sprite{ get => _sprite; }

        public virtual async UniTask HandleUseItem()
        {
            Debug.Log("�A�C�e������v���܂���");
            return;
        }
    }
}

