using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GimmickAction;

public class GimmickAction : MonoBehaviour
{
    /// <summary>
    /// �M�~�b�N�ɋ߂Â����ہA�{�^����\������C���^�[�t�F�[�X
    /// </summary>
    public interface IGimmick
    {
        string DisplayButton();
        void ActivateGimmick();
    }
    
}

