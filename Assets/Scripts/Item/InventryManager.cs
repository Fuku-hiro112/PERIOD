using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Test
{
    public class InventryManager : MonoBehaviour
    {
        public static InventryManager s_Instance;//HACK: staticŽ«‚ß‚½‚¢

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
        /// ƒAƒCƒeƒ€ŒðŠ·
        /// </summary>
        public void ItemTrade()
        {
            //‚O”Ô–Ú‚Æ‚P”Ô‚ß‚ðŒðŠ·
            int boyItemID = _boyInventroy.ItemIDs[0];
            _boyInventroy.ItemIDs[0] = _engineerInventroy.ItemIDs[1];
            _engineerInventroy.ItemIDs[1] = boyItemID;

            EngineerInventroy.InventryUI.UpdateUI();
            BoyInventroy.InventryUI.UpdateUI();
        }
    }
}
