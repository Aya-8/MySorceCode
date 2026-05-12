using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(AudioSource))]
public class MousePosCheck : MonoBehaviour, IPointerClickHandler
{
    private bool isLink = false;
    private GameObject _memoWindow;
    private GameObject _memoDetail;

    private void Awake()
    {
        _memoWindow = GameObject.Find("MemoWindow").gameObject;
        _memoDetail = GameObject.Find("喫茶スイートピーCanvas").gameObject;
        if(_memoWindow == null)Debug.Log("memoWindow is null");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 _mousePos = Input.mousePosition;

        TextMeshProUGUI _text = GetComponent<TextMeshProUGUI>();
        
        //renderModeを代入する
        

        int _index = TMP_TextUtilities.FindIntersectingLink(_text, _mousePos, eventData.pressEventCamera);

        if (_index == -1)
        {
            isLink = false;
            Debug.Log("これはリンクが埋め込まれてない" + _index);
        }
        else
        {
            isLink = true;
            Debug.Log("これはリンク！！！！！！" + _index);
            _memoWindow.GetComponent<FrontPopUp>().BringFront();
            Debug.Log("メモに書かれている文字:"+_text);
            /*_memoDetail.SetActive(true);
            _memoDetail.GetComponent<FrontPopUp>().BringFront();*/
            
        }

    }

    public bool IsLink()
    {
        return isLink;
    }
}
