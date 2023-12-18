using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class Slot : MonoBehaviour
    {
        private Image _icon;
        private int _itemID;

        private void Awake()
        {
            // �q�I�u�W�F�N�g��ItemImage���擾
            _icon = transform.GetChild(0).gameObject.GetComponent<Image>();
            //RectTransform trans = GetComponent<RectTransform>();
            //trans.sizeDelta *= new Vector2(1.2f, 1.2f);
        }
        private void SetItem(int itemID, Sprite itemSprite)
        {
            _itemID = itemID;
            _icon.sprite = itemSprite;
        }
        /// <summary>
        /// �X���b�g�ɃA�C�e����ǉ�
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="itemSprite"></param>
        public void AddItem(int itemID, Sprite itemSprite)
        {
            SetItem(itemID, itemSprite);
        }
        /// <summary>
        /// �X���b�g�̃A�C�e�����폜
        /// </summary>
        public void ClearSlot()
        {
            int nullID = InventryManager.s_NullID;
            SetItem(nullID, null);
        }
        /// <summary>
        /// �A�C�e���g�p
        /// </summary>
        public void UseItem()
        {
            ClearSlot();
        }
    }
}