using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Item
{
    public class InventryManager : MonoBehaviour
    {
        public static InventryManager s_Instance;//HACK: static辞めたい
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
            //boyIndexとenginnerIndexのアイテムを交換
            int boyItemID = _boyInventroy.ItemIDs[0];
            _boyInventroy.ItemIDs[0] = _engineerInventroy.ItemIDs[0];
            _engineerInventroy.ItemIDs[0] = boyItemID;

            // 両UI更新
            EngineerInventroy.InventryUI.UpdateUI();
            BoyInventroy.InventryUI.UpdateUI();
        }
        /// <summary>
        /// アイテム交換
        /// </summary>
        /// <param name="boyIndex"></param>
        /// <param name="enginnerIndex"></param>
        public void SwapItem(int boyIndex, int enginnerIndex)
        {
            //boyIndexとenginnerIndexのアイテムを交換
            int boyItemID = _boyInventroy.ItemIDs[boyIndex];
            _boyInventroy.ItemIDs[boyIndex] = _engineerInventroy.ItemIDs[enginnerIndex];
            _engineerInventroy.ItemIDs[boyIndex] = boyItemID;

            // 両UI更新
            EngineerInventroy.InventryUI.UpdateUI();
            BoyInventroy.InventryUI.UpdateUI();
        }
        public void UseItem(Inventry inventry,int index)
        {
            ItemDataManager.s_Instance.SearchProsess(inventry.ItemIDs[index]).Forget();//TODO: Prosessの引数をアイテムIDに変える
            inventry.Remove(inventry.ItemIDs[index]);
        }
        //HACK: このままだとInventryの関数の意味がなくなってしまうので改良が必要
        /// <summary>
        /// インベントリに空きがあればアイテムを入れる
        /// </summary>
        /// <param name="itemID">インベントリに追加するアイテムID</param>
        /// <returns>インベントリに空きがあったらTrue</returns>
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
        /// インベントリにスペースがあるかどうかを確認する
        /// </summary>
        /// <param name="inventroy">どのインベントリを調べるか</param>
        /// <returns>スペースがあったらtrue</returns>
        private (bool, int) IsSpaceInventory(Inventry inventroy)
        {
            for (int i = 0; i < inventroy.ItemIDs.Count; i++)
            {
                // 空きがあれば
                if (inventroy.ItemIDs[i] == s_NullID)
                {
                    return (true, i);
                }
            }
            return (false, -1);
        }
    }
}
