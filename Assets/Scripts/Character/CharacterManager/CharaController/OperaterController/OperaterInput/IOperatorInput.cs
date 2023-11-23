using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UIElements;

namespace Character
{
    interface IOperatorInput
    {
        public Vector3 MovementValue
        {
            get; set;
        }

        public void OnStart();
        public bool IsGimmickAction();
        public bool IsChange();
    }
}
