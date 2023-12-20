using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Item
{
    public class ItemControllerTest : MonoBehaviour
    {
        [SerializeField] private int _itemID = -1;
        
        /// <summary>
        /// ƒAƒCƒeƒ€‚ð“üŽè‚·‚é
        /// </summary>
        public void ObtainItem(Inventry inventroy)
        {
            inventroy.Add(_itemID);
        }
    }
}
