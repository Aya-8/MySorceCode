using UnityEngine;
using UnityEngine.EventSystems;

public class GetSystemMessageManager : MonoBehaviour, IPointerClickHandler
{ 
    //どのヒロインか
    private string id;

    //何に関するメッセージか
    private string system;
    
    private GameObject mainWindow;

    /// <summary>
    /// 受け取った文章から誰に関する何のメッセージかを読み取る
    /// </summary>
    /// <param name="message"></param>
    public void CheckMessage(string message)
    {
        id = null;
        system = null;
        
        switch (message)　//誰に関するか
        {
            case string msg when message.Contains("真白"): id = "A"; break;
            case string msg when message.Contains("ましろ"): id = "A"; break;
            case string msg when message.Contains("日葵"): id = "B"; break;
            case string msg when message.Contains("らむね"): id = "C"; break;
            case string mag when message.Contains("萩香"): id = "D"; break;
            case string mag when message.Contains("S.Minase"): id = "E"; break;
            case string mag when message.Contains("紫苑"): id = "E"; break;
        }

        switch (message)　//何に関するか
        {
            case string msg when message.Contains("メッセージ"): system = "Rine"; break;
            case string msg when message.Contains("度"): system = "Love"; break;
        }
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"[Click] system={system ?? "null"}, id={id ?? "null"}");
        mainWindow = GameObject.Find("Main Windows"); //mainWindows(各キャンバスを統括する親)を探す
        if(system == "Rine")OpenRineWindow(id);
        if(system == "Love")OpenLoveChecker(id);
    }

    //SMの情報によって、Rineを開くメゾット
    private void OpenRineWindow(string id)
    {
        Transform talk = mainWindow.transform.Find("Heroin"+id+"RineWindow");
        talk.GetComponent<FrontPopUp>().BringFront();
    }

    private void OpenLoveChecker(string id)
    {
        Transform canvas = mainWindow.transform.Find("LoveCheckerCanvas");
        Transform back = canvas.transform.Find("backColorImage");
        Transform tab = canvas.transform.Find("Meters");
        
        
        canvas.GetComponent<FrontPopUp>().BringFront();
        
        back.GetComponent<ChangeColor>().BackColor(id);
        
        tab.GetComponent<MeterView>().Meter(id);
    }

    public string Id
    {
        get { return id; }
    }
    
    
}