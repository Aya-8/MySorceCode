using System;
using QFramework;
using UnityEngine;
using TMPro;
using Events.AddMemo;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MemoListView : MonoBehaviour,IController
{
    [SerializeField] private GameObject memoPrefab;
    [SerializeField] private Transform content;
    [SerializeField]private GameObject memoWindow;

    private IUnRegister _unreg;
    
    private Dictionary<string, GameObject> memoCanvas = new Dictionary<string, GameObject>();
    

    private void Start()
    {
        foreach (Transform child in memoWindow.transform)//memoWindowの子ノードのうちCanvasのみをdictionaryに格納
        { 
            if (child.GetComponent<Canvas>() != null)
            {
                memoCanvas[child.name] = child.GameObject();
                memoCanvas[child.name].SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        //メモに単語を追加するイベントを購読する
        _unreg = this.RegisterEvent<MemoAddEvent>(e => SpawnCard(e.Word));
    }

    private void OnDisable()
    {
        _unreg?.UnRegister();
        _unreg = null;
    }

    private void SpawnCard(string word)
    {
        //Debug.Log("spawn"+ word);
        GameObject newMemo = Instantiate(memoPrefab, content);
        TextMeshProUGUI memo = newMemo.GetComponentInChildren<TextMeshProUGUI>(true);
        if (memo) memo.text = word;

        //メモ追加と同時にMemoWindowをPopUp
        memoWindow.GetComponent<FrontPopUp>().BringFront();
        memoWindow.GetComponent<AudioSource>().Play(); //メモ追加SE
        
        DisplayCanvas(word);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(content);
    } 
    
    public void DisplayCanvas(string word)//word + Canvas名のCanvasを表示する関数(元々の)
    {
        if (memoCanvas == null) return;

        
        if (memoCanvas.ContainsKey(word + "Canvas"))
        {
            HideAllCanvas();
            memoCanvas[word + "Canvas"].SetActive(true);
            //Debug.Log(word + "Canvas is active");デバック用
        }
        else
        {
            Debug.Log("該当の単語" + word + "はありません");
        }
    }
    
    public void HideAllCanvas()//全てのwordCanvasを非表示にする関数
    {
        if (memoCanvas == null) return;
        foreach (GameObject memo in memoCanvas.Values)
        {
            memo.SetActive(false);
            //Debug.Log("False");デバック用
        }
    }
    
    
    public IArchitecture GetArchitecture() => GameArchitecture.Interface;

    
}



