using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace Test
{
    // インベントリの画像を管理
    public class InventryUITest : MonoBehaviour
    {
        [SerializeField]
        private Transform slotsParent;
        private InventroyTest _inventroy = default;
        public SlotTest[] Slots { get; private set; }

        private void Start()
        {
            // slotsParentの子を全てSlotsに格納
            TryGetComponent(out _inventroy);
            Slots = slotsParent.GetComponentsInChildren<SlotTest>();
            UpdateUI();
        }
        /// <summary>
        /// インベントリの画像を更新する
        /// </summary>
        public void UpdateUI()
        {
            Debug.Log(_inventroy.IItemSearcher,this);
            for (int i = 0; i < Slots.Length; i++)
            {
                if (i < _inventroy.ItemIDs.Count)
                {
                    int itemID = _inventroy.ItemIDs[i];
                    Sprite itemSprite = _inventroy.IItemSearcher.SearchSprite(itemID);
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
