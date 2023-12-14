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
    public class CharacterChange // ��ɖ��O��ύX
    {
        private IOperatorInput _iOperat0rInput;

        public CharacterChange(OperatorInput input)
        {
            _iOperat0rInput = input;
        }

        /// <summary>
        /// ���͂��󂯂�
        /// </summary>
        /// <returns></returns>
        public bool OnChange()
        {
            return _iOperat0rInput.IsChange();
        }
    }
}
