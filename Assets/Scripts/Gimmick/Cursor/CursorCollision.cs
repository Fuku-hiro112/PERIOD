using Input;
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
        // �M�~�b�N�f�[�^�H�H
        [SerializeField]
        Transform _startPosition;

        public void MoveFirstPosition(GameObject cursor, CursorInput input)
        {
            cursor.transform.position = _startPosition.position;
            input.Vibration();
        }
        public void EndGimmick()
        {
            // �I�����o�@�I�������
            // �p�l���A�M�~�b�N����\����
        }
    }
}
