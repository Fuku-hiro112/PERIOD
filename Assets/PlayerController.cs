using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody _rigidbody;
    private Vector3 _movement;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// 移動処理
    /// </summary>
    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movement = new Vector3(movementVector.x, 0.0f, movementVector.y);
    }

    void Update()
    {
        _rigidbody.velocity = _movement * _speed;

        ActionButton();
    }

    /// <summary>
    /// アクションボタン処理
    /// </summary>
    public void ActionButton()
    {
        // ゲームパッドが接続されていないとnull。
        if (Gamepad.current == null) return;

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("△が押された！");
        }
        if (Gamepad.current.buttonNorth.wasReleasedThisFrame)
        {
            Debug.Log("△が離された！");
        }


        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("×が押された！");
        }
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("×が離された！");
        }


        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            Debug.Log("□が押された！");
        }
        if (Gamepad.current.buttonWest.wasReleasedThisFrame)
        {
            Debug.Log("□が離された！");
        }


        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            Debug.Log("〇が押された！");
        }
        if (Gamepad.current.buttonEast.wasReleasedThisFrame)
        {
            Debug.Log("〇が離された！");
        }

    }


    /// <summary>
    /// キャラクター変更処理 
    /// </summary>
    /* public void ChangeCharacter()
    {
        // ゲームパッドが接続されていないとnull。
        if (Gamepad.current == null) return;

        if (Gamepad.current.leftShoulder.wasPressedThisFrame)
        {
            Debug.Log("L1が押された！");
        }
        if (Gamepad.current.leftShoulder.wasReleasedThisFrame)
        {
            Debug.Log("L1が離された！");
        }
        if (Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            Debug.Log("R1が押された！");
        }
        if (Gamepad.current.rightShoulder.wasReleasedThisFrame)
        {
            Debug.Log("R1が離された！");
        }
    } */


    /// <summary>
    /// アイテム選択処理
    /// </summary>
    /* public void SelectItem()
    {
        // ゲームパッドが接続されていないとnull。
        if (Gamepad.current == null) return;

        if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            Debug.Log("L2が押された！");
        }
        if (Gamepad.current.leftTrigger.wasReleasedThisFrame)
        {
            Debug.Log("L2が離された！");
        }

        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            Debug.Log("R2が押された！");
        }
        if (Gamepad.current.rightTrigger.wasReleasedThisFrame)
        {
            Debug.Log("R2が離された！");
        }
    } */


    /// <summary>
    /// アイテム使用処理
    /// </summary>
   /* public void UseItem()
    {
        // ４つある十字ボタンのどこに設定するか分からなかったのでまだ設定していません。
    } */

}
