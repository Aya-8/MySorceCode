using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForConversation.Views
{
    public class RineName:MonoBehaviour
    {
        [SerializeField] string id;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Image unread;
        
        public string Id => id;

        void Awake()
        {
            unread.enabled = false;
        }
        
        //idを引数としてRineの名前リストに追加するメゾット
        public void AddList()
        {
            gameObject.SetActive(true);
        }
        
        public void Rename(string name)
        {
            text.text = name;
        }
        

        public void EnableUnread(bool enable)
        {
            unread.enabled = enable;
        }
    }
}