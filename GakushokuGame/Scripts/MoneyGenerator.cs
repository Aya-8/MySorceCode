using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class MoneyGenerator: MonoBehaviour
{
    [SerializeField] TMP_Text moneyText; //ランダムで生成される所持金を表示する
    [SerializeField] GameObject okText;  //確認テキスト
    
    private float time = 3.0f;
    private bool count = false;

    private int _initialMoney;
    public AudioClip MoneySE;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        RandomMoney();
        this.aud = GetComponent<AudioSource>();
        ScoreManager.Instance.ResetAll();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && !count)
        {
            count = true;
            this.aud.PlayOneShot(this.MoneySE);
            moneyText.GetComponent<TextMeshProUGUI>().text = _initialMoney.ToString() + "yen";

        }

        if(count)
        {
            time -= Time.deltaTime;
        }

        if (time < 1 && count)
        {
            this.okText.SetActive(true);
        }

        if(time < 0 && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    //ランダムで所持金を決定する
    private void RandomMoney()
    {
        int dice = Random.Range(20, 35);
        _initialMoney = dice * 100;
        ScoreManager.Instance.InitialMoney =_initialMoney;
    }

}
