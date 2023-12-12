using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Test
{
    // WARNING: InventryUI�͂��̃X�N���v�g�Ɠ��l�̃I�u�W�F�N�g�ɃA�^�b�`���邱�� 15�s�ڂ��
    // �C���x���g���̏������@�ǉ���폜�̊֐����ĂуC���x���g���ɃA�C�e����ۑ�����
    public class InventroyTest : MonoBehaviour
    {
        //public static InventroyTest s_Instance;//HACK: static���߂���
        public List<int> ItemIDs = new List<int>();
        public InventryUITest InventryUI { get; private set; }//WARNING: InventryUI�͂��̃X�N���v�g�Ɠ��l�̃I�u�W�F�N�g�ɃA�^�b�`���邱��
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
            Assert.IsNotNull(IItemSearcher,"IItemSearcher��Null�ł�");
            // ���ꂪ�A�^�b�`����Ă���obj����Get���Ă��邱�Ƃ𖾎�
            InventryUI = this.gameObject.GetComponent<InventryUITest>();
        }
        /// <summary>
        /// �A�C�e���ǉ�
        /// </summary>
        /// <param name="itemID">�ǉ��������A�C�e��ID��n��</param>
        public void Add(int itemID)
        {
            ItemIDs.Add(itemID);
            InventryUI.UpdateUI();
        }
        /// <summary>
        /// �A�C�e���폜
        /// </summary>
        /// <param name="itemID">�폜�������A�C�e��ID��n��</param>
        public void Remove(int itemID)
        {
            ItemIDs.Remove(itemID);
            InventryUI.UpdateUI();
        }

        //TODO: InventroyManager�ŃA�C�e���̌��������N�ƃG���W�j�A�ōs��
    }
}
