using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGimmick : GimmickAction
{ 
    public void DisplayButton()
    {
        Debug.Log("�H�{�^���ŏ��� / �~���"); // �Ή�����{�^����\��
    }

    public void ActivateGimmick()
    {
        // ��q�̃M�~�b�N���A�N�e�B�u�ɂȂ����Ƃ��̏���
        Debug.Log("��q������ / �~���");
    }
}

