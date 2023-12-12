using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public partial class DropItem : MonoBehaviour
    {
        int itemID;

        private void Start()
        {
            //GetComponent<Image>().sprite = item.icon;
        }

        public void PickUpItem(bool isBoy)//TODO: Chara‚ÉisBoy‚ð‚Â‚¯‚é
        {
            if (isBoy)
            {
                InventryManager.s_Instance.BoyInventroy.Add(itemID);
                Destroy(gameObject);
            }
            else
            {
                InventryManager.s_Instance.EngineerInventroy.Add(itemID);
                Destroy(gameObject);
            }
        }
    }
}