using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffGimmick : GimmickAction
{
    public string DisplayButton()
    {
        return "�H�{�^���œn��"; // �Ή�����{�^����\��
    }

    public void ActivateGimmick()
    {
        // �R�̃M�~�b�N���A�N�e�B�u�ɂȂ����Ƃ��̏���
        Debug.Log("�R��n��");
    }
}
