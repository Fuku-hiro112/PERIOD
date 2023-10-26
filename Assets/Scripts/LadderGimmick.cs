using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LadderGimmick : MonoBehaviour, IGimmick
{

    [SerializeField] Text _guideText;
    [SerializeField] GameObject _guideCanvas;
    private Text _text;
    Vector3 _position;
    private float _posUp = 2f;
    public void DisplayButton(Vector3 pos)
    {
        if (_text == null)
        {
            _text = Instantiate(_guideText);
            _text.transform.parent = _guideCanvas.transform;
            _text.text = "□ボタンを押せ";
        }
        _position = pos;
        _position.y = _posUp;
    }

    public void ActivateGimmick(bool isInput)
    {
        // 梯子のギミックがアクティブになったときの処理
        if(isInput) Debug.Log("梯子を昇る / 降りる");
    }

    void Update()
    {
        if(_text != null)
        _text.transform.position = _position;
    }

    
}

