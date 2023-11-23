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
        IOperaterInput _IOperaterInput;

        public CharacterChange(OperaterInput input)
        {
            _IOperaterInput = input;
        }

        /// <summary>
        /// “ü—Í‚ðŽó‚¯‚é
        /// </summary>
        /// <returns></returns>
        public bool OnChange()
        {
            return _IOperaterInput.IsChange();
        }
    }
}
