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
        // �A�C�e���̌���������
        if(_isBoyItemSelecting)
        {
            // ���N�̃C���x���g������I��
            SelectInventryItem(_inventryManager.BoyInventroy);
            if (CharacterManager.OperatorInput.ItemUse())// �A�C�e���g�p�{�^���������ꂽ
            {

            }
            if (CharacterManager.OperatorInput.ItemSwap())// �����{�^���������ꂽ
            {
                // �G���W�j�A�̃C���x���g������I���o����悤��
                _inventryManager.EngineerInventroy.InventryUI.SizeUpable = true;
                _isBoyItemSelecting = false;
            }
        }
        else
        {
            // �G���W�j�A�̃C���x���g������I��
            SelectInventryItem(_inventryManager.EngineerInventroy);
            if (CharacterManager.OperatorInput.ItemSwap())// �����{�^���������ꂽ
            {
                InventryUI boy = _inventryManager.BoyInventroy.InventryUI;
                InventryUI enginner = _inventryManager.EngineerInventroy.InventryUI;
                enginner.SizeUpable = false;// �T�C�Y��ύX�\�ɂ��邩
                // �A�C�e������
                _inventryManager.SwapItem(boy.CurrentIndex,enginner.CurrentIndex);
                _isBoyItemSelecting = true;
            }
        }

        // �I����Ԃ�������悤�ɏ����傫������

    }

    /// <summary>
    /// �C���x���g���̃A�C�e����I��
    /// </summary>
    /// <param name="inventroy"></param>
    private void SelectInventryItem(Inventry inventroy)
    {
        if (CharacterManager.OperatorInput.ItemSelectMove() >= 1)// ��̗��
        {
            InventryUI ui = inventroy.InventryUI;
            // 1��ڂɂ��鎞�A��ɓ������Ȃ��悤�ɂ���
            if (ui.CurrentIndex > 0)
            {
                ui.CurrentIndex--;
                // �C���x���g����ݒ蒆��UI�����@UI��傫������
                ui.UpdateUISize(ui.CurrentIndex);
            }
        }
        if (CharacterManager.OperatorInput.ItemSelectMove() <= -1)
        {
            InventryUI ui = inventroy.InventryUI;
            // ��ԉ��̗�ɂ��鎞�A���ɓ������Ȃ��悤��
            if (ui.CurrentIndex < inventroy.ItemIDs.Count - 1)
            {
                ui.CurrentIndex++;
                // �C���x���g����ݒ蒆��UI�����@UI��傫������
                inventroy.InventryUI.UpdateUISize(ui.CurrentIndex);
            }
        }
    }
}
