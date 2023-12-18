using Item;
using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField]
    private Inventroy _inventroy;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            other.gameObject.GetComponent<ItemControllerTest>()?.ObtainItem(_inventroy);
        }
    }
}
