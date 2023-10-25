using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBoardGimmick : GimmickAction
{
    public void DisplayButton()
    {
        Debug.Log( "？ボタンで作動させる"); // 対応するボタンを表示
    }

    public void ActivateGimmick()
    {
        // 配電盤のギミックがアクティブになったときの処理
        Debug.Log("配電盤を作動させる");
    }
}
