using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class Slot : MonoBehaviour
    {
        private Image _icon;
        private int _itemID;
        public RectTransform Transform;

        //[SerializeField]
        //private Vector2 _sizeMultiple = new Vector2(1.2f, 1.2f);

        private void Awake()
        {
            // 子オブジェクトのItemImageを取得
            _icon = transform.GetChild(0).gameObject.GetComponent<Image>();
            Transform = GetComponent<RectTransform>();
        }
        private void SetItem(int itemID, Sprite itemSprite)
        {
            _itemID = itemID;
            _icon.sprite = itemSprite;
        }
        /// <summary>
        /// スロットにアイテムを追加
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="itemSprite"></param>
        public void AddItem(int itemID, Sprite itemSprite)
        {
            SetItem(itemID, itemSprite);
        }
        /// <summary>
        /// スロットのアイテムを削除
        /// </summary>
        public void ClearSlot()
        {
            int nullID = InventryManager.s_NullID;
            SetItem(nullID, null);
        }
        /// <summary>
        /// アイテム使用
        /// </summary>
        public void UseItem()
        {
            ClearSlot();
        }
        public void ChengeSize(Vector2 sizeDelta)
        {
            Transform.sizeDelta = sizeDelta;
        }
    }
}