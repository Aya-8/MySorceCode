using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;//UI表示に必要

public class GameDirector : MonoBehaviour
{

    [SerializeField] private GameObject timerText;
    private float _timeLimit = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(MenuShow.easy)_timeLimit = 15.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this._timeLimit -= Time.deltaTime;
        this.timerText.GetComponent<TextMeshProUGUI>().text = this._timeLimit.ToString("F1");

        if (_timeLimit < 0) EndMainGame();

        //デバック用キー
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    //タイマーが0になったときに呼ばれる ゲーム終了の処理
    private void EndMainGame()
    {
        ScoreManager.Instance.DecideResult();
        SceneManager.LoadScene("Resultscene");
    }



}
