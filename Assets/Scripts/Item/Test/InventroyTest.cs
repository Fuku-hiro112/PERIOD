using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Test
{
    // WARNING: InventryUIはこのスクリプトと同様のオブジェクトにアタッチすること 15行目より
    // インベントリの情報を持つ　追加や削除の関数を呼びインベントリにアイテムを保存する
    public class InventroyTest : MonoBehaviour
    {
        //public static InventroyTest s_Instance;//HACK: static辞めたい
        public List<int> ItemIDs = new List<int>();
        public InventryUITest InventryUI { get; private set; }//WARNING: InventryUIはこのスクリプトと同様のオブジェクトにアタッチすること
        public IItemSearcher IItemSearcher { get; private set; }

        private void Awake()
        {
            IItemSearcher = ItemDataManager.s_Instance;
        }
        private void Start()
        {
            Debug.Log(ItemDataManager.s_Instance,this);
            IItemSearcher = ItemDataManager.s_Instance;
            Debug.Log(IItemSearcher,this);
            Assert.IsNotNull(IItemSearcher,"IItemSearcherはNullです");
            // これがアタッチされているobjからGetしていることを明示
            InventryUI = this.gameObject.GetComponent<InventryUITest>();
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
