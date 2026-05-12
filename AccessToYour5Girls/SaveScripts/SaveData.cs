using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QFramework;

//セーブする要素をまとめたクラス
[Serializable]
public class SaveData
{
    //ヒロインたちのパラメータ
    public HeroineStatus[] heroines = new HeroineStatus[5];
    
    
    //ノードの進み具合
    public List<string> completedNodeId = new List<string>();
    
    //現在時刻
    //時
    public int hour;
    //分
    public int minute;
    //時間
    public string time;
    
    //日付
    public int day;

    //ヒロインのステータスを保持する変数のインスタンスの作成
    public SaveData()
    {
        for (int i = 0; i < heroines.Length; i++)
        {
            heroines[i] = new HeroineStatus();
        }
    }
}

[Serializable]
public class HeroineStatus
{
    public string id;
    public int affinity;
    public int trust;
    public int yandere;
}

