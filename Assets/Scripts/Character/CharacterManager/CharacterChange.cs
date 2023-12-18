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
    public class CharacterChange // Œã‚É–¼‘O‚ğ•ÏX
    {
        private IOperatorInput _iOperat0rInput;

        public CharacterChange(OperatorInput input)
        {
            _iOperat0rInput = input;
        }

        /// <summary>
        /// “ü—Í‚ğó‚¯‚é
        /// </summary>
        /// <returns></returns>
        public bool OnChange()
        {
            return _iOperat0rInput.IsChange();
        }
    }
}
