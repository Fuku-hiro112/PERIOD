using Cysharp.Threading.Tasks;
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
        private Inventry _boyInventroy = default;
        [SerializeField]
        private Inventry _engineerInventroy = default;

        public Inventry BoyInventroy { get => _boyInventroy; }
        public Inventry EngineerInventroy { get => _engineerInventroy; }

        private void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
            }
        }

        public void TestItemSwap()
        {
            //boyIndex��enginnerIndex�̃A�C�e��������
            int boyItemID = _boyInventroy.ItemIDs[0];
            _boyInventroy.ItemIDs[0] = _engineerInventroy.ItemIDs[0];
            _engineerInventroy.ItemIDs[0] = boyItemID;

            // ��UI�X�V
            EngineerInventroy.InventryUI.UpdateUI();
            BoyInventroy.InventryUI.UpdateUI();
        }
        /// <summary>
        /// �A�C�e������
        /// </summary>
        /// <param name="boyIndex"></param>
        /// <param name="enginnerIndex"></param>
        public void SwapItem(int boyIndex, int enginnerIndex)
        {
            //boyIndex��enginnerIndex�̃A�C�e��������
            int boyItemID = _boyInventroy.ItemIDs[boyIndex];
            _boyInventroy.ItemIDs[boyIndex] = _engineerInventroy.ItemIDs[enginnerIndex];
            _engineerInventroy.ItemIDs[boyIndex] = boyItemID;

            // ��UI�X�V
            EngineerInventroy.InventryUI.UpdateUI();
            BoyInventroy.InventryUI.UpdateUI();
        }
        public void UseItem(Inventry inventry,int index)
        {
            ItemDataManager.s_Instance.SearchProsess(inventry.ItemIDs[index]).Forget();//TODO: Prosess�̈������A�C�e��ID�ɕς���
            inventry.Remove(inventry.ItemIDs[index]);
        }
        //HACK: ���̂܂܂���Inventry�̊֐��̈Ӗ����Ȃ��Ȃ��Ă��܂��̂ŉ��ǂ��K�v
        /// <summary>
        /// �C���x���g���ɋ󂫂�����΃A�C�e��������
        /// </summary>
        /// <param name="itemID">�C���x���g���ɒǉ�����A�C�e��ID</param>
        /// <returns>�C���x���g���ɋ󂫂���������True</returns>
        public bool IsAddItem(int itemID)
        {
            (bool IsSpaceBoy, int indexBoy) = IsSpaceInventory(_boyInventroy);
            (bool IsSpaceEngineer, int indexEngineer) = IsSpaceInventory(EngineerInventroy);

            if (IsSpaceBoy) _boyInventroy.Add(indexBoy, itemID);
            else if (IsSpaceEngineer) _engineerInventroy.Add(indexEngineer, itemID);
            else return false;

            return true;
        }

        /// <summary>
        /// �C���x���g���ɃX�y�[�X�����邩�ǂ������m�F����
        /// </summary>
        /// <param name="inventroy">�ǂ̃C���x���g���𒲂ׂ邩</param>
        /// <returns>�X�y�[�X����������true</returns>
        private (bool, int) IsSpaceInventory(Inventry inventroy)
        {
            for (int i = 0; i < inventroy.ItemIDs.Count; i++)
            {
                // �󂫂������
                if (inventroy.ItemIDs[i] == s_NullID)
                {
                    return (true, i);
                }
            }
            return (false, -1);
        }
    }
}
