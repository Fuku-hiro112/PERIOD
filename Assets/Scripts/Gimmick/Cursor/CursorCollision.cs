using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gimmick
{
    [Serializable]
    public class CursorCollision
    {
        // ギミックデータ？？
        [SerializeField]
        Transform _startPosition;

        public void MoveFirstPosition(GameObject cursor)
        {
            cursor.transform.position = _startPosition.position;
        }
        public void EndGimmick()
        {
            // 終了演出　終わったら
            // パネル、ギミックが非表示に
        }
    }
}
