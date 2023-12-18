using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class ItemSourceDataBase : ScriptableObject
    {
        [SerializeField]// アイテムのID
        private int _id;
        [SerializeField]// アイテム名
        private string _name;
        [SerializeField]// アイテム画像(インベントリ表示時)
        private Sprite _sprite;

        public int ID       { get => _id; }
        public string Name  { get => _name; }
        public Sprite Sprite{ get => _sprite; }

        public virtual async UniTask HandleUseItem()
        {
            Debug.Log("アイテムが一致しません");
            return;
        }
    }
}

