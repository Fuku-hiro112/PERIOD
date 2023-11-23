using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ギミックのタグを判別するクラス
/// </summary>
public class DisplayGimmickError : MonoBehaviour
{
    [SerializeField]
    private Image _ErrorMessage;

    private void Start()
    {
        if (_ErrorMessage == null)
        {
            _ErrorMessage = GetComponent<Image>();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayErrorMessage();
        }
    }

    /// <summary>
    /// 操作できないことを画像で表示
    /// </summary>
    private void DisplayErrorMessage()
    {
        if (_ErrorMessage != null)
        {
            _ErrorMessage.gameObject.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ErrorMessage.gameObject.SetActive(false);
        }
    }
}

