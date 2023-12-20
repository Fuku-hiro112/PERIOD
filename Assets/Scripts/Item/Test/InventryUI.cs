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
        [SerializeField]
        private Vector2 _slotSizeMultiple = new Vector2(1.2f, 1.2f);
        [SerializeField]
        private RectTransform _sampleSlot;
        private Vector2 _slotOriginalSize;
        private Inventry _inventroy = default;
        private IItemSearcher _iItemSearcher;//MEMO: 宣言時の代入はAwakeよりも早い
        private OriginalSlot _slot;
        // 初回のみ値を変更出来るように
        private class OriginalSlot
        {
            public readonly Vector2 Size;
            public OriginalSlot(Vector2 slotSize) { Size = slotSize; }
        }
        public int CurrentIndex = -1;// InventrySelectから変更
        public bool SizeUpable = false;
        public Slot[] Slots { get; private set; }

        private void Awake()
        {
            _slot = new OriginalSlot(_sampleSlot.sizeDelta);
        }
        private void Start()
        {
            _slotOriginalSize = _sampleSlot.sizeDelta;
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
        
        /// <summary>
        /// アイテム設定中処理 サイズを大きくする
        /// </summary>
        /// <param name="slotIndex"></param>
        public void UpdateUISize(int slotIndex)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (slotIndex == i)// 選択中
                {
                    if (!SizeUpable) Slots[i].ChengeSize(_slotOriginalSize);
                    // でかいサイズに
                    Slots[i].ChengeSize(_slot.Size);
                }
                else
                {
                    // 通常サイズに
                    Slots[i].ChengeSize(_slotOriginalSize);
                }
            }
        }
    }
}
