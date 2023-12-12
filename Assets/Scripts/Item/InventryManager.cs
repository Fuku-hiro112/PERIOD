using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Test
{
    public class InventryManager : MonoBehaviour
    {
        public static InventryManager s_Instance;//HACK: static辞めたい

        [SerializeField]
        private InventroyTest _boyInventroy = default;
        [SerializeField]
        private InventroyTest _engineerInventroy = default;

        public InventroyTest BoyInventroy { get => _boyInventroy; }
        public InventroyTest EngineerInventroy { get => _engineerInventroy; }

        private void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
            }
        }

        /// <summary>
        /// アイテム交換
        /// </summary>
        public void ItemTrade()
        {
            //０番目と１番めを交換
            int boyItemID = _boyInventroy.ItemIDs[0];
            _boyInventroy.ItemIDs[0] = _engineerInventroy.ItemIDs[1];
            _engineerInventroy.ItemIDs[1] = boyItemID;

            EngineerInventroy.InventryUI.UpdateUI();
            BoyInventroy.InventryUI.UpdateUI();
        }
    }
}
