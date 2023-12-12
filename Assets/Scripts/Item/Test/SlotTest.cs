using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotTest : MonoBehaviour
{
    public Image _icon;
    public int ItemID { get; private set; }

    private const int c_nullID = -1;

    private void Start()
    {
        _icon = GetComponent<Image>();
        //RectTransform trans = GetComponent<RectTransform>();
        //trans.sizeDelta *= new Vector2(1.2f, 1.2f);
    }
    private void SetItem(int itemID,Sprite itemSprite)
    {
        ItemID = itemID;
        _icon.sprite = itemSprite;
    }
    /// <summary>
    /// �X���b�g�ɃA�C�e����ǉ�
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="itemSprite"></param>
    public void AddItem(int itemID, Sprite itemSprite)
    {
        SetItem(itemID, itemSprite);
    }
    /// <summary>
    /// �X���b�g�̃A�C�e�����폜
    /// </summary>
    public void ClearSlot()
    {
        SetItem(c_nullID, null);
    }
    /// <summary>
    /// �A�C�e���g�p
    /// </summary>
    public void UseItem()
    {
        ClearSlot();

    }
}
