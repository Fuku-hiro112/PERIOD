using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGimmick : GimmickAction
{ 
    public void DisplayButton()
    {
        Debug.Log("？ボタンで昇る / 降りる"); // 対応するボタンを表示
    }

    public void ActivateGimmick()
    {
        // 梯子のギミックがアクティブになったときの処理
        Debug.Log("梯子を昇る / 降りる");
    }
}

