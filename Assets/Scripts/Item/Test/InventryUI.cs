using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace Item
{
    // インベントリの画像を管理
    public class InventryUI : MonoBehaviour
    {
        [SerializeField]
        private Transform slotsParent;
        private Inventroy _inventroy = default;
        public Slot[] Slots { get; private set; }
        private IItemSearcher _iItemSearcher;//MEMO: 宣言時の代入はAwakeよりも早い

        private void Start()
        {
            _iItemSearcher = ItemDataManager.s_Instance;//WARNING: インスタンスをAwakeで作成しているため実行順的にStateに書かないとNullが返る
            // slotsParentの子を全てSlotsに格納
            this.gameObject.TryGetComponent(out _inventroy);// 分かりやすくするためthis
            Slots = slotsParent.GetComponentsInChildren<Slot>();
            UpdateUI();
        }
        /// <summary>
        /// インベントリの画像を更新する
        /// </summary>
        public void UpdateUI()
        {
            Debug.Log(_iItemSearcher,this);
            for (int i = 0; i < Slots.Length; i++)// Slotの長さ分
            {
                if (i < _inventroy.ItemIDs.Count)
                {
                    // i番目のスロットに画像とIDを挿入
                    int itemID = _inventroy.ItemIDs[i];
                    Sprite itemSprite = _iItemSearcher.SearchSprite(itemID);
                    Slots[i].AddItem(itemID, itemSprite);
                }
                else
                {
                    Slots[i].ClearSlot();
                }
            }
        }
    }
}
