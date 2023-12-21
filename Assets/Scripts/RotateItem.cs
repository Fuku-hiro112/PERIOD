using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    [SerializeField] int rot;
    void Start()
    {

    }
    void Update()
    {
        transform.Rotate(0, rot, 0);
    }
}
