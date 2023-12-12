using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace Test
{
    // �C���x���g���̉摜���Ǘ�
    public class InventryUITest : MonoBehaviour
    {
        [SerializeField]
        private Transform slotsParent;
        private InventroyTest _inventroy = default;
        public SlotTest[] Slots { get; private set; }

        private void Start()
        {
            // slotsParent�̎q��S��Slots�Ɋi�[
            TryGetComponent(out _inventroy);
            Slots = slotsParent.GetComponentsInChildren<SlotTest>();
            UpdateUI();
        }
        /// <summary>
        /// �C���x���g���̉摜���X�V����
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
