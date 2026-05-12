using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenTalkRoom : MonoBehaviour
{
    private GameObject _mashiroTalkRoom;
    private GameObject _himariTalkRoom;
    private GameObject _ramuneTalkRoom;
    private GameObject _sionTalkRoom;
    private GameObject _shukaTalkRoom;
    [SerializeField] private PopUpFunction _popUpFunction;
    
    // Start is called before the first frame update
    void Start()
    {
        _mashiroTalkRoom = transform.Find("MashiroTalkRoom").gameObject;
        _himariTalkRoom = transform.Find("HimariTalkRoom").gameObject;
        _ramuneTalkRoom = transform.Find("RamuneTalkRoom").gameObject;
        _sionTalkRoom = transform.Find("SonTalkRoom").gameObject;
        _shukaTalkRoom = transform.Find("ShukaTalkRoom").gameObject;
    }

    public void PopTalkRoom()
    {
        _popUpFunction.popUpFunction();
    }
    
}
