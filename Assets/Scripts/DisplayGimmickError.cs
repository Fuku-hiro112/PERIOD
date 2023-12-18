using Gimmick;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DisplayGimmickError : MonoBehaviour
{
    GameObject _myCanvas;

    Image _imgError;

    [SerializeField]
    private Image _ErrorMessage;

    [SerializeField]
    GameObject ErrorMessagePrefab;

    [SerializeField]
    Transform Canvas;

    private bool _isBoy;

    private void Start()
    {
        _myCanvas = Instantiate(ErrorMessagePrefab, Canvas.transform, false);　// （引数：エラーメッセージ、親子の位置、ワールドポジションにいるか）
        _imgError = _myCanvas.transform.Find("ErrorMessage").GetComponentInChildren<Image>();
        if (_imgError != null)
        {  
            _imgError.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Gimmick"))
        {
            GimmickController gimmickController;
            other.gameObject.TryGetComponent(out gimmickController);

            if(gimmickController?.Available == Gimmick.Character.Boy)
            {
                if (_isBoy)
                {
                    // TODO: 合っているときの処理
                }
                if (!_isBoy)
                {
                    // TODO: 間違っているときの処理
                }
            }
            if (gimmickController?.Available == Gimmick.Character.Engineer)
            {
                if (!_isBoy)
                {
                    // TODO: 合っているときの処理
                    _ErrorMessage?.gameObject.SetActive(true);
                }
                if (_isBoy)
                {
                    // TODO: 間違っているときの処理
                }
            }
            if (gimmickController?.Available == Gimmick.Character.Both)
            {
                // TODO: 合っているときの処理
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Gimmick"))
        {
            if (_ErrorMessage != null)
            {
                _ErrorMessage.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {-
        _myCanvas.transform.forward = Camera.main.transform.forward;　// 画面正面に表示する
    }
}

