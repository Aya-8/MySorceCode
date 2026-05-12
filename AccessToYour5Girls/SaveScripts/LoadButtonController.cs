using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class LoadButtonController : MonoBehaviour,IController
{
    [SerializeField] private SaveDataManager slm;

    [Header("Debug")]
    [SerializeField] bool isDebug = false;

    public void Load()
    {
        slm.Data = slm.JsonRead();
        
        var nm = this.GetModel<INodeHistoryModel>();
        var sm = this.GetModel<IStatsModel>();
        var gtm = this.GetModel<IGameTimeModel>();
        var cm = this.GetModel<ICalendarModel>();
        var cs = this.GetSystem<IConversationSystem>();
        
        
        gtm.Hour.Value = slm.Data.hour;
        gtm.Minute.Value = slm.Data.minute;
        gtm.Time.Value = slm.Data.time; 
        Debug.Log(slm.Data.day == null?"slm.Data.dayなし":"slm.Data.dayあり");
        cm.SetCurrentDay(slm.Data.day);
        
        string[] heroineID = {"A","B","C","D","E"};

        for (int i = 0; i < 5; i++)
        {
            slm.Data.heroines[i].id = heroineID[i];
            
            sm.SetAffinity(slm.Data.heroines[i].id,slm.Data.heroines[i].affinity, false);
            sm.SetTrust(slm.Data.heroines[i].id,slm.Data.heroines[i].trust, false); 
            sm.SetYandere(slm.Data.heroines[i].id,slm.Data.heroines[i].yandere,  false);
            
        }
        
        Debug.Log("時間:"+slm.Data.hour);
        Debug.Log("分:"+slm.Data.minute);
        Debug.Log("タイム:"+slm.Data.time);
        Debug.Log("日付:"+slm.Data.day);
        Debug.Log("好感度"+slm.Data.heroines[0].affinity);
        
        /*int t = 0;
        foreach (var NodeId in slm.Data.completedNodeId)
        {
            nm.MarkCompleted(NodeId);
            Debug.Log("ロードしたノード：" + string.Join(", ", slm.Data.completedNodeId));
            t++;
        }*/

        StartCoroutine(Test(cs,cm));
    }

    private IEnumerator Test(IConversationSystem cs, ICalendarModel cm)
    {
        cs.StopConversation();
        
        RineChatView.Instance.ClearByDay("A", cm.CurrentDay.Value +1);
        RineChatView.Instance.ClearByDay("B", cm.CurrentDay.Value +1);
        RineChatView.Instance.ClearByDay("C", cm.CurrentDay.Value +1);
        RineChatView.Instance.ClearByDay("D", cm.CurrentDay.Value +1);
        RineChatView.Instance.ClearByDay("E", cm.CurrentDay.Value +1);
        
        yield return new WaitForSeconds(0.6f);
        
        TriggerSavepoint(slm.Data.day);
    }

    private void TriggerSavepoint(int day)
    {
        string nodeId = day <= 9 ? $"day0{day}_SavePoint" : $"day{day}_SavePoint";
        Debug.Log("Debug"+isDebug);
        if(isDebug) nodeId = $"TestSavePoint.Chat";
        this.SendCommand(new FireEventNodeCommand(nodeId));
    }
    
    public IArchitecture GetArchitecture() => GameArchitecture.Interface;
}
