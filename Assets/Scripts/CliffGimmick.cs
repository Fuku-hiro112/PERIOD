using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffGimmick : GimmickAction
{
    public string DisplayButton()
    {
        return "？ボタンで渡る"; // 対応するボタンを表示
    }

    public void ActivateGimmick()
    {
        // 崖のギミックがアクティブになったときの処理
        Debug.Log("崖を渡る");
    }
}
