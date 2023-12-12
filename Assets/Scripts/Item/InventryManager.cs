using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Test
{
    public class InventryManager : MonoBehaviour
    {
        public static InventryManager s_Instance;//HACK: static���߂���

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
        /// �A�C�e������
        /// </summary>
        public void ItemTrade()
        {
            //�O�ԖڂƂP�Ԃ߂�����
            int boyItemID = _boyInventroy.ItemIDs[0];
            _boyInventroy.ItemIDs[0] = _engineerInventroy.ItemIDs[1];
            _engineerInventroy.ItemIDs[1] = boyItemID;

            EngineerInventroy.InventryUI.UpdateUI();
            BoyInventroy.InventryUI.UpdateUI();
        }
    }
}
