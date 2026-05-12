using System;
using UnityEngine;
using QFramework;
using System.Collections.Generic;

public class SaveButtonController : MonoBehaviour,IController
{
    [SerializeField] private SaveDataManager slm;

    private static SaveButtonController _instance;
    public static SaveButtonController Instance => _instance;
    void Awake()
    {
        _instance = this;
    }

    //ボタンを押したときに各所からデータを集める
    public void Save()
    { 
        var nm = this.GetModel<INodeHistoryModel>();
        var sm = this.GetModel<IStatsModel>(); 
        var gtm = this.GetModel<IGameTimeModel>();
        var cm = this.GetModel<ICalendarModel>();
        

        slm.Data.hour = gtm.Hour.Value;
        slm.Data.minute = gtm.Minute.Value;
        slm.Data.time = gtm.Time.Value;
        slm.Data.day = cm.CurrentDay.Value;
        
        Debug.Log("時:"+slm.Data.hour);
        Debug.Log("分:"+slm.Data.minute);
        Debug.Log("時刻："+slm.Data.time); 
        Debug.Log("日付："+slm.Data.day);
        
        string[] heroineID = {"A","B","C","D","E"};

        for (int i = 0; i < 5; i++)
        {
            slm.Data.heroines[i].id = heroineID[i];
            
            slm.Data.heroines[i].affinity = sm.GetAffinity(slm.Data.heroines[i].id);
            slm.Data.heroines[i].trust = sm.GetTrust(slm.Data.heroines[i].id);
            slm.Data.heroines[i].yandere = sm.GetYandere(slm.Data.heroines[i].id);
            
        }
        
        for (int i = 0; i < 5; i++)
        {
            string id = heroineID[i];
            Debug.Log("ヒロイン "+id+" : 好感度："+slm.Data.heroines[i].affinity);
            Debug.Log("ヒロイン "+id+" : 信頼度："+slm.Data.heroines[i].trust);
            Debug.Log("ヒロイン "+id+" : ヤンデレ度:"+slm.Data.heroines[i].yandere);
            
        }
        
        Debug.Log($"[NodeHistory] Completed Count: {slm.Data.completedNodeId.Count}");
        int t = 0;
        foreach (var NodeId in nm.AllCompleted)
        {
            if (!slm.Data.completedNodeId.Contains(NodeId))
            {
                slm.Data.completedNodeId.Add(NodeId);
            }

            Debug.Log("完了したノード：" + string.Join(", ", slm.Data.completedNodeId));
            t++;
        }
        slm.JsonRewrite();
    }
    
    public IArchitecture GetArchitecture() => GameArchitecture.Interface;
}
