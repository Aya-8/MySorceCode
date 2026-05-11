using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultGenerator : MonoBehaviour
{
    [SerializeField] private Text _resultMessage;
    [SerializeField] GameObject UsedMoneyText;
    [SerializeField] GameObject HaveMoneyText;
    [SerializeField] GameObject HadMoneyText;

    public AudioClip moneySE;
    AudioSource aud;
    float timer = 4;
    int difference;
    
    //public int Pay = GameDirector.Pay;


    // Start is called before the first frame update
    void Start()
    { 

        Debug.Log("start");
        this.aud = GetComponent<AudioSource>();
        this.aud.PlayOneShot(this.moneySE);

        this.HadMoneyText.GetComponent<TextMeshProUGUI>().text = ScoreManager.Instance.InitialMoney.ToString() + "yen";
    }

    
    // Update is called once per frame
    void Update()
    {
        

        timer -= Time.deltaTime;
        if(timer < 3)
        {
            //使ったお金
            this.UsedMoneyText.GetComponent<TextMeshProUGUI>().text = ScoreManager.Instance.ResultMoney.ToString() + "yen";
        }
        if(timer < 1)
        {
            //所持金と使ったお金の差
            difference = ScoreManager.Instance.InitialMoney - Mathf.Abs(ScoreManager.Instance.ResultMoney);
            this.HaveMoneyText.GetComponent<TextMeshProUGUI>().text = difference.ToString() + "yen";
        }
        if(timer < 0)
        {
            ResultMessage(difference);
        }
        if(timer < 0 &&Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    //現在の所持金によってメッセージを表示する

    private void ResultMessage(int difference)
    {
        switch (difference)
        {
            case < 0:
                _resultMessage.text = "皿洗いで返しな！！";
                break;
            case 0:
                _resultMessage.text = "ピッタリ！";
                break;
            case <= 500:
                _resultMessage.text = "ナイス！";
                break;
            case <= 1000:
                _resultMessage.text = "惜しい！";
                break;
            default:
                _resultMessage.text = "チキってんなよ！！";
                break;
        }
    }
}
