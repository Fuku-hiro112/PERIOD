using Character;
using DG.Tweening.Core.Easing;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventrySelecter : MonoBehaviour
{
    private InventryManager _inventryManager;
    private bool _isBoyItemSelecting = false;

    private int _currentInventryBoy = 0; 
    private int _currentInventryEnginner = -1;
    
    private void Start()
    {
        TryGetComponent(out _inventryManager);
        _inventryManager.BoyInventroy.InventryUI.SizeUpable = true;
    }
    
    private void Update()
    {
        // アイテムの交換をする
        if(_isBoyItemSelecting)
        {
            // 少年のインベントリから選択
            SelectInventryItem(_inventryManager.BoyInventroy);
            if (CharacterManager.OperatorInput.ItemUse())// アイテム使用ボタンが押された
            {

            }
            if (CharacterManager.OperatorInput.ItemSwap())// 交換ボタンが押された
            {
                // エンジニアのインベントリから選択出来るように
                _inventryManager.EngineerInventroy.InventryUI.SizeUpable = true;
                _isBoyItemSelecting = false;
            }
        }
        else
        {
            // エンジニアのインベントリから選択
            SelectInventryItem(_inventryManager.EngineerInventroy);
            if (CharacterManager.OperatorInput.ItemSwap())// 交換ボタンが押された
            {
                InventryUI boy = _inventryManager.BoyInventroy.InventryUI;
                InventryUI enginner = _inventryManager.EngineerInventroy.InventryUI;
                enginner.SizeUpable = false;// サイズを変更可能にするか
                // アイテム交換
                _inventryManager.SwapItem(boy.CurrentIndex,enginner.CurrentIndex);
                _isBoyItemSelecting = true;
            }
        }

        // 選択状態が分かるように少し大きくする

    }

    /// <summary>
    /// インベントリのアイテムを選択
    /// </summary>
    /// <param name="inventroy"></param>
    private void SelectInventryItem(Inventry inventroy)
    {
        if (CharacterManager.OperatorInput.ItemSelectMove() >= 1)// 上の列へ
        {
            InventryUI ui = inventroy.InventryUI;
            // 1列目にいる時、上に動かさないようにする
            if (ui.CurrentIndex > 0)
            {
                ui.CurrentIndex--;
                // インベントリを設定中のUI処理　UIを大きくする
                ui.UpdateUISize(ui.CurrentIndex);
            }
        }
        if (CharacterManager.OperatorInput.ItemSelectMove() <= -1)
        {
            InventryUI ui = inventroy.InventryUI;
            // 一番下の列にいる時、下に動かさないように
            if (ui.CurrentIndex < inventroy.ItemIDs.Count - 1)
            {
                ui.CurrentIndex++;
                // インベントリを設定中のUI処理　UIを大きくする
                inventroy.InventryUI.UpdateUISize(ui.CurrentIndex);
            }
        }
    }
}
