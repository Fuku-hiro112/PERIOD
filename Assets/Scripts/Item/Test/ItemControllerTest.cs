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
        /// アイテムを入手する
        /// </summary>
        public void ObtainItem(Inventry inventroy)
        {
            inventroy.Add(0, _itemID);
        }
    }
}
