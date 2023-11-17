using Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool OnChange()
    {
        return _IOperaterInput.IsChange();
    }
    


}
