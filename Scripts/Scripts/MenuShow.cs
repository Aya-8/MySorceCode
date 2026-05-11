using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuShow : MonoBehaviour
{
    [SerializeField] GameObject OKText; //確認用テキスト

    float time = 2.0f;
    public static bool easy = false;
    // Start is called before the first frame update
    void Start()
    {
        OKText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            OKText.SetActive(true);   
        }

        //Eキーを押したらeasyモード
        if(Input.GetKeyDown(KeyCode.E))　
        {
            MenuShow.easy = true;
        }

        //Nキーを押したらnormalモード
        if (Input.GetKeyDown(KeyCode.N))
        {
            MenuShow.easy = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }

        //デバック用キー
        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
