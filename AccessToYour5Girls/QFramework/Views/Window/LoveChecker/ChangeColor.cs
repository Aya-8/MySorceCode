using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    
    [SerializeField] private Image _backColorImage; //ヒロインごとに背景色を変えるためのimage
    
    [Header("イメージカラー")]
    [SerializeField] private Color _white = Color.white;
    [SerializeField] private Color _pink = Color.magenta;
    [SerializeField] private Color _yellow = Color.yellow;
    [SerializeField] private Color _blue = Color.blue;
    [SerializeField] private Color _violet = new Color(0.7f, 0.5f, 0.8f);

    private string _nowColor;
    
    public void white()
    {
        _backColorImage.color = _white;
        _nowColor = "White";
    }
    
    public void pink()
    {
        _backColorImage.color = _pink;
        _nowColor = "Pink";
    }
    
    public void yellow()
    {
        _backColorImage.color = _yellow;
        _nowColor = "Yellow";
    }
    
    public void blue()
    {
        _backColorImage.color = _blue;
        _nowColor = "Blue";
    }
    
    public void violet()
    {
        _backColorImage.color = _violet;
        _nowColor = "Violet";
    }

    
    public string isColor() //今誰が選択されているか取得できる
    {
        string color = _nowColor switch
        {
            "White" => "A",
            "Yellow" => "B",
            "Pink" => "C",
            "Blue" => "D",
            "Violet" => "E",
            _ => "A"
        };
        return color;
    }

    public void BackColor(string id)
    {
        switch (id)
        {
            case "A": white(); break;
            case "B": yellow(); break;
            case "C": pink(); break;
            case "D": blue(); break;
            case "E": violet(); break;
        }
    }
}