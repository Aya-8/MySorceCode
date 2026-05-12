using System.Collections.Generic;
using ForConversation.Views;
using UnityEngine;
using TMPro;

public class RineNameView : MonoBehaviour
{
    [SerializeField] List<RineName> rineNameList = new List<RineName>();
    
    [Header("Debug")]
    [SerializeField] bool debug = false;

    void Update()
    { 
        //Debug
        if (Input.GetKey(KeyCode.A)&&debug)
        {
            GetRineName("A").AddList();
        }

        if (Input.GetKeyDown(KeyCode.B)&&debug)
        {
            GetRineName("B").AddList();
        }

        if (Input.GetKeyDown(KeyCode.X)&&debug)
        {
            GetRineName("A").Rename("ましろちゃん");
        }
        
    }

    public RineName GetRineName(string id)
    {
        return rineNameList.Find(r => r.Id == id);
    }
}
