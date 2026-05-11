using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int _initialMoney; Å@//MoneyGeneratorÇ©ÇÁèâä˙ã‡äzÇÇ‡ÇÁÇ§
    private int _nowPay = 0;    //åªç›ÇÃéxï•Ç¢ã‡äz
    private int _resultMoney; //ç≈èIìIÇ»èäéùã‡äz

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }

    public void DecideDish(string dish)
    {
        switch (dish)
        {
            case "Katsu":
                GetKatsu(); break;
            case "Sarada":
                GetSarada(); break;
            case "Curry":
                GetCurry(); break;
            case "Misosoup":
                GetMisosoup(); break;
            case "Soba":
                Getsoba(); break;
            case "Ramen":
                GetRamen(); break;
            default:
                Debug.Log("ÇªÇÒÇ»éMÇÕÇ»Ç¢"); break;
        }
    }

    public void GetKatsu()
    {
        this._nowPay += 500;

    }

    public void GetSarada()
    {
        this._nowPay += 100;
    }

    public void GetCurry()
    {
        this._nowPay += 200;
    }

    public void GetMisosoup()
    {
        this._nowPay += 100;
    }

    public void Getsoba()
    {
        this._nowPay += 400;
    }
    public void GetRamen()
    {
        this._nowPay += 300;
    }

    public int InitialMoney
    {
        set { _initialMoney = value; }
        get { return _initialMoney; }
    }

    public int ResultMoney
    {
        set { _resultMoney = value; }
        get { return _resultMoney; }
    }

    public void DecideResult()
    {
        _resultMoney = _nowPay;
    }

    public void ResetAll()
    {
        _nowPay = 0;
        _resultMoney = 0;
    }
}
