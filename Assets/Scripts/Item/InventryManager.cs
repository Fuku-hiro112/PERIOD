using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Item
{
    public class InventryManager : MonoBehaviour
    {
        public static InventryManager s_Instance;//HACK: static���߂���
        public static readonly int s_NullID = -1;

        [SerializeField]
        private Inventroy _boyInventroy = default;
        [SerializeField]
        private Inventroy _engineerInventroy = default;

        public Inventroy BoyInventroy { get => _boyInventroy; }
        public Inventroy EngineerInventroy { get => _engineerInventroy; }

        private void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
            }
        }

        /// <summary>
        /// �A�C�e������
        /// </summary>
        /// <param name="boyIndex"></param>
        /// <param name="enginnerIndex"></param>
        public void ItemTrade(int boyIndex, int enginnerIndex)
        {
            //boyIndex��enginnerIndex�̃A�C�e��������
            int boyItemID = _boyInventroy.ItemIDs[boyIndex];
            _boyInventroy.ItemIDs[boyIndex] = _engineerInventroy.ItemIDs[enginnerIndex];
            _engineerInventroy.ItemIDs[boyIndex] = boyItemID;

            // ��UI�X�V
            EngineerInventroy.InventryUI.UpdateUI();
            BoyInventroy.InventryUI.UpdateUI();
        }

        //HACK: ���̂܂܂���Inventry�̊֐��̈Ӗ����Ȃ��Ȃ��Ă��܂��̂ŉ��ǂ��K�v
        /// <summary>
        /// �C���x���g���ɋ󂫂�����΃A�C�e��������
        /// </summary>
        /// <param name="itemID">�C���x���g���ɒǉ�����A�C�e��ID</param>
        /// <returns>�C���x���g���ɋ󂫂���������True</returns>
        public bool IsAddItem(int itemID)
        {
            if      (IsSpaceInventory(_boyInventroy))      _boyInventroy.Add(itemID);
            else if (IsSpaceInventory(_engineerInventroy)) _engineerInventroy.Add(itemID);
            else return false;

            return true;
        }

        /// <summary>
        /// �C���x���g���ɃX�y�[�X�����邩�ǂ������m�F����
        /// </summary>
        /// <param name="inventroy">�ǂ̃C���x���g���𒲂ׂ邩</param>
        /// <returns>�X�y�[�X����������true</returns>
        private bool IsSpaceInventory(Inventroy inventroy)
        {
            for (int i = 0; i < inventroy.ItemIDs.Count; i++)
            {
                // �󂫂������
                if (inventroy.ItemIDs[i] != s_NullID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}