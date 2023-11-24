using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsGet : MonoBehaviour
{
    [SerializeField] Bounds bounds;
    void Start()
    {
        bounds = this.GetComponent<MeshFilter>().mesh.bounds;
        Debug.Log("バウンス"+bounds);
        Debug.Log("マックス"+bounds.max);
        Debug.Log("ミン"+bounds.min);
        Debug.Log("センター" + bounds.center);
        Debug.Log("サイズ" + bounds.size);
    }
}
