using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    Vector3 origiral;
    void Start()
    {
        //origiral = transform.position;
        //origiral -= new Vector3(0,4.9f+5.9f,0);
    }

    
    void Update()
    {
        //transform.position = _player.transform.position - origiral;
    }
}
