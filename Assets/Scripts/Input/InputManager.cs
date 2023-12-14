using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    private InputActionMap _cuurentMap;
    private InputActionMap _playerMap;
    private InputActionMap _uiMap;
    [SerializeField] private bool _isChange = false;
    /*
    private void Awake()
    {
        _playerMap = _input.actions.FindActionMap("Player");
        _uiMap = _input.actions.FindActionMap("UI");
    }
    */
    public void ActionMapChange(string map)
    {
        _cuurentMap = _input.currentActionMap;
        Debug.Log(_cuurentMap);
        _input.SwitchCurrentActionMap(map);    
        Debug.Log("•Ï‰»Œã" + _cuurentMap);
    }
}
