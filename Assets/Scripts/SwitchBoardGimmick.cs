using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBoardGimmick : GimmickAction
{
    public string DisplayButton()
    {
        return "�H�{�^���ō쓮������"; // �Ή�����{�^����\��
    }

    public void ActivateGimmick()
    {
        // �z�d�Ղ̃M�~�b�N���A�N�e�B�u�ɂȂ����Ƃ��̏���
        Debug.Log("�z�d�Ղ��쓮������");
    }
}
