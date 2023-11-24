using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ギミックタグを判別するクラス
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
    Transform Canvas; //Canvas の位置


    private void Start()
    {
        _myCanvas = Instantiate(ErrorMessagePrefab);
        _myCanvas.transform.SetParent(gameObject.transform); //Canvasを自身の子構造に
        _myCanvas.transform.localPosition = transform.localPosition + Canvas.position; //キャンバスの位置補正
        _imgError = _myCanvas.transform.Find("ErrorMessage").GetComponent<Image>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("当たった");

            _imgError.gameObject.SetActive(true);

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _imgError.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        _myCanvas.transform.forward = Camera.main.transform.forward;
    }
}

