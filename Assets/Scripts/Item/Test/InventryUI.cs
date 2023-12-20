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
        [SerializeField]
        private Vector2 _slotSizeMultiple = new Vector2(1.2f, 1.2f);
        [SerializeField]
        private RectTransform _sampleSlot;
        private Vector2 _slotOriginalSize;
        private Inventry _inventroy = default;
        private IItemSearcher _iItemSearcher;//MEMO: �錾���̑����Awake��������
        private OriginalSlot _slot;
        // ����̂ݒl��ύX�o����悤��
        private class OriginalSlot
        {
            public readonly Vector2 Size;
            public OriginalSlot(Vector2 slotSize) { Size = slotSize; }
        }
        public int CurrentIndex = -1;// InventrySelect����ύX
        public bool SizeUpable = false;
        public Slot[] Slots { get; private set; }

        private void Awake()
        {
            _slot = new OriginalSlot(_sampleSlot.sizeDelta);
        }
        private void Start()
        {
            _slotOriginalSize = _sampleSlot.sizeDelta;
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
        
        /// <summary>
        /// �A�C�e���ݒ蒆���� �T�C�Y��傫������
        /// </summary>
        /// <param name="slotIndex"></param>
        public void UpdateUISize(int slotIndex)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (slotIndex == i)// �I��
                {
                    if (!SizeUpable) Slots[i].ChengeSize(_slotOriginalSize);
                    // �ł����T�C�Y��
                    Slots[i].ChengeSize(_slot.Size);
                }
                else
                {
                    // �ʏ�T�C�Y��
                    Slots[i].ChengeSize(_slotOriginalSize);
                }
            }
        }
    }
}
