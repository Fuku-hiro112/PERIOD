using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace Item
{
    // �C���x���g���̉摜���Ǘ�
    public class InventryUI : MonoBehaviour
    {
        [SerializeField]
        private Transform slotsParent;
        private Inventroy _inventroy = default;
        public Slot[] Slots { get; private set; }
        private IItemSearcher _iItemSearcher;//MEMO: �錾���̑����Awake��������

        private void Start()
        {
            _iItemSearcher = ItemDataManager.s_Instance;//WARNING: �C���X�^���X��Awake�ō쐬���Ă��邽�ߎ��s���I��State�ɏ����Ȃ���Null���Ԃ�
            // slotsParent�̎q��S��Slots�Ɋi�[
            this.gameObject.TryGetComponent(out _inventroy);// ������₷�����邽��this
            Slots = slotsParent.GetComponentsInChildren<Slot>();
            UpdateUI();
        }
        /// <summary>
        /// �C���x���g���̉摜���X�V����
        /// </summary>
        public void UpdateUI()
        {
            Debug.Log(_iItemSearcher,this);
            for (int i = 0; i < Slots.Length; i++)// Slot�̒�����
            {
                if (i < _inventroy.ItemIDs.Count)
                {
                    // i�Ԗڂ̃X���b�g�ɉ摜��ID��}��
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
