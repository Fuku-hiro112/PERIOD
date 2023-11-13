using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GimmickTest : MonoBehaviour
{
    Image _image;
    Rigidbody2D _rb;
    [SerializeField]
    float _speed ;

    void Start()
    {
        _image = GetComponent<Image>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float speed = Time.deltaTime * _speed;
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed,0,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -speed, 0);
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other != null)
        {
            _image.color = Color.red;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other != null)
        {
            _image.color = Color.white;
        }
    }
}
