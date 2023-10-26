using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// ギミックに近づいた際、ボタンを表示するインターフェース
/// </summary>
interface IGimmick
 {
    void DisplayButton(Vector3 pos);
    void  ActivateGimmick(bool input);
 } 


