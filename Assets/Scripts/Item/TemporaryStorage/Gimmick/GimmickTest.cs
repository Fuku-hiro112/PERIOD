using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GimmickTest : MonoBehaviour
{
    Image _image;
    Rigidbody2D _rb;
    [SerializeField]
    float _speed ;
    [SerializeField]
    private GameObject _inputObj;
    private PlayerInput _input;
    public Vector3 MoveValue { get; private set; }
    private Vector3 _originalPos;

    void Start()
    {
        _input = _inputObj.GetComponent<PlayerInput>();
        _image = GetComponent<Image>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _originalPos = transform.position;
        _inputObj.TryGetComponent(out _input);
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMoveStop;
    }
    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
    }
    void OnMoveStop(InputAction.CallbackContext context)
    {
        MoveValue = Vector3.zero;
    }
    void OnMove(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();// ’l‚ÌŽæ“¾
        MoveValue = new Vector3(inputValue.x, inputValue.y, 0);
    }
    void Update()
    {
        transform.position += MoveValue * Time.deltaTime * _speed;
        Debug.Log($"{transform.position}");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other, Color.red);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        OnTrigger(other, Color.white);
    }

    private void OnTrigger(Collider2D other, Color coloer)
    {
        if (other != null)
        {
            other.gameObject.transform.parent.GetComponent<Image>().color = coloer;
            transform.position = _originalPos;
        }
    }
}
