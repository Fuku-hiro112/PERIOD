using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsGet : MonoBehaviour
{
    [SerializeField] Bounds bounds;
    void Start()
    {
        bounds = this.GetComponent<MeshFilter>().mesh.bounds;
        Debug.Log("�o�E���X"+bounds);
        Debug.Log("�}�b�N�X"+bounds.max);
        Debug.Log("�~��"+bounds.min);
        Debug.Log("�Z���^�[" + bounds.center);
        Debug.Log("�T�C�Y" + bounds.size);
    }
}
