using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GimmickAction;

public class GimmickAction : MonoBehaviour
{
    /// <summary>
    /// ギミックに近づいた際、ボタンを表示するインターフェース
    /// </summary>
    public interface IGimmick
    {
        string DisplayButton();
        void ActivateGimmick();
    }
    
}

