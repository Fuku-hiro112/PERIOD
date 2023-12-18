using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class AvailableItem : MonoBehaviour
    {
        [SerializeField]
        private int itemID;

        private void Start()
        {
            //GetComponent<Image>().sprite = item.icon;
        }
        public void PickUp()//TODO: CharaにisBoyをつける
        {
            if (InventryManager.s_Instance.IsAddItem(itemID))
            {
                Destroy(gameObject);
            }
            else
            {
                //TODO: アイテム入手負荷処理　ビーブ音など
            }
        }
    }
}