using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Item
{
    // WARNING: InventryUIはこのスクリプトと同様のオブジェクトにアタッチすること 15行目より
    // インベントリの情報を持つ　追加や削除の関数を呼びインベントリにアイテムを保存する
    public class Inventroy : MonoBehaviour
    {
        //public static InventroyTest s_Instance;//HACK: static辞めたい
        public List<int> ItemIDs = new List<int>();
        public InventryUI InventryUI { get; private set; }//WARNING: InventryUIはこのスクリプトと同様のオブジェクトにアタッチすること

        private void Awake()
        {
        }
        private void Start()
        {
            // これがアタッチされているobjからGetしていることを明示
            InventryUI = this.gameObject.GetComponent<InventryUI>();
        }
        /// <summary>
        /// アイテム追加
        /// </summary>
        /// <param name="itemID">追加したいアイテムIDを渡す</param>
        public void Add(int itemID)
        {
            ItemIDs.Add(itemID);
            InventryUI.UpdateUI();
        }
        /// <summary>
        /// アイテム削除
        /// </summary>
        /// <param name="itemID">削除したいアイテムIDを渡す</param>
        public void Remove(int itemID)
        {
            ItemIDs.Remove(itemID);
            InventryUI.UpdateUI();
        }

        //TODO: InventroyManagerでアイテムの交換を少年とエンジニアで行う
    }
}
