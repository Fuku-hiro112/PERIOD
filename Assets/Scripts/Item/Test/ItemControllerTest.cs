using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

namespace Item
{
    public class ItemControllerTest : MonoBehaviour
    {
        [SerializeField] private int _itemID = -1;
        
        /// <summary>
        /// ƒAƒCƒeƒ€‚ð“üŽè‚·‚é
        /// </summary>
        public void ObtainItem(InventroyTest inventroy)
        {
            inventroy.Add(_itemID);
        }
    }
}
