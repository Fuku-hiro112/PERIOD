using Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CharacterChange
    {
        IOperatorInput _IOperat0rInput;

        public CharacterChange(OperatorInput input)
        {
            _IOperat0rInput = input;
        }

        /// <summary>
        /// “ü—Í‚ðŽó‚¯‚é
        /// </summary>
        /// <returns></returns>
        public bool OnChange()
        {
            return _IOperat0rInput.IsChange();
        }
    }
}
