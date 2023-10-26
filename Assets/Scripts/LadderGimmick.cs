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
            _text.text = "���{�^��������";
        }
        _position = pos;
        _position.y = _posUp;
    }

    public void ActivateGimmick(bool isInput)
    {
        // ��q�̃M�~�b�N���A�N�e�B�u�ɂȂ����Ƃ��̏���
        if(isInput) Debug.Log("��q������ / �~���");
    }

    void Update()
    {
        if(_text != null)
        _text.transform.position = _position;
    }

    
}

