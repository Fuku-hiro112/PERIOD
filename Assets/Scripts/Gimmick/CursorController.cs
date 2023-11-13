using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmick
{
    //using Input;

    [Serializable]
    public class CursorController
    {
        [SerializeField]
        private GameObject _cursor;
        [SerializeField]
        private CursorInput _cursorInput;
        [SerializeField]
        private float _speed = 1;
        //[SerializeField]
        //private CursorController _cursorController = new CursorController();

        public void OnStart()
        {
            
        }

        public void OnUpdate()
        {
            _cursor.transform.position += _cursorInput.MoveValue * Time.deltaTime * _speed;
            Debug.Log(_cursorInput.MoveValue);
        }
    }
}
