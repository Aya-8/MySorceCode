using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    //[SerializeField]
    private string filePath;
    private SaveData save;
    public string fileName = "SaveData.json";

    //開始時にファイルチェックと読み込み
    void Awake()
    {
        //パス名を取得
        filePath = Application.persistentDataPath + "/" + fileName;
        
        save = new SaveData();
        
        
    }

    //ゲームの内容をセーブする
    public void JsonRewrite()
    {
        string json = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(json);
        sw.Flush();
        sw.Close();
        
        Debug.Log($"[SaveLoadManager] {fileName} saved!");
        Debug.Log(filePath);
    }

    //セーブしたJSONの内容をロードする
    public SaveData JsonRead()
    {
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string savedData = sr.ReadToEnd();
            sr.Close();
            return JsonUtility.FromJson<SaveData>(savedData);
        }
        else
        {
            Debug.LogError("Couldn't read JSON file!");
            return null;
        }
    }

    //
    public SaveData Data
    {
        get
        {
            return save;
        }
        set
        {
            save = value;
        }
    }
}
