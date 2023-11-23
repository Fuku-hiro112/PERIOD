using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ギミックのタグを判別するクラス
/// </summary>
public class DisplayGimmickError : MonoBehaviour
{
    GameObject _myCanvas; //自身のCanvas
    Image _imgError; 

    [SerializeField]
    private Image _ErrorMessage;

    [SerializeField]
    GameObject ErrorMessagePrefab;

    [SerializeField]
    Vector3 CanvasPos = new Vector3(0, 2, 0); //Canvas の位置


    private void Start()
    {
        _myCanvas = Instantiate(ErrorMessagePrefab);
        _myCanvas.transform.SetParent(gameObject.transform); //Canvasを自身の子構造に
        _myCanvas.transform.position = transform.position + CanvasPos; //キャンバスの位置補正
        _imgError = _myCanvas.transform.Find("ErrorMessage").GetComponent<Image>();
    }


    private void Update()
    {
        _myCanvas.transform.forward = Camera.main.transform.forward;
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

