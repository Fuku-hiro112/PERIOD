using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="ScriptableObject/Create")]
public class itemTest : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
}
