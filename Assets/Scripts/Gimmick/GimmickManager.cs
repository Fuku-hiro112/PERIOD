using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmick
{
    public class GimmickManager : MonoBehaviour
    {
        [SerializeField]
        private CursorController _cursorController = new CursorController();
        [SerializeField]
        private GimmickController _gimmickController = new GimmickController();

        void Start()
        {
            _cursorController.OnStart();
            _gimmickController.OnStart();
        }
        void Update()
        {
            _cursorController.OnUpdate();
            _gimmickController.OnUpdate();
        }
    }
}