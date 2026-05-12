using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

public class MeterView : MonoBehaviour,IController
{ 
    [SerializeField] private Slider _affinitySlider;
    [SerializeField] private Slider _trustSlider;
    [SerializeField] private Slider _yandereSlider;
    [SerializeField] private ChangeColor  _changeColor;
    
    public IArchitecture GetArchitecture() => GameArchitecture.Interface;

    public void Meter(string id)
    {
        _affinitySlider.gameObject.SetActive(true);
        _trustSlider.gameObject.SetActive(true);
        _yandereSlider.gameObject.SetActive(true);
        
        _affinitySlider.minValue = 0;
        _affinitySlider.maxValue = 100;
        
        _trustSlider.minValue = 0;
        _trustSlider.maxValue = 100;
        
        _yandereSlider.minValue = 0;
        _yandereSlider.maxValue = 100;
        
        var _statsModel = this.GetModel<IStatsModel>();
        
        //現在のステータスをsliderに反映
        _affinitySlider.value = _statsModel.GetAffinity(id);
        _trustSlider.value = _statsModel.GetTrust(id);
        _yandereSlider.value = _statsModel.GetYandere(id);
        
        _statsModel.OnHeroineChanged.Register(stat =>
        {
            //LoveCheckerで選択しているヒロインと通知されたHeroinIdが異なっていたらsliderを変更しない
            if (_changeColor.isColor() != stat.HeroineId) return; 
            
            //変更されたステータスをsliderに反映
            switch(stat.Item)
            {
                case StatItem.Affinity:
                    _affinitySlider.value = stat.Value;
                    break;
                case StatItem.Trust:
                    _trustSlider.value = stat.Value;
                    break;
                case StatItem.Yandere:
                    _yandereSlider.value = stat.Value;
                    break;
            };
        }).UnRegisterWhenGameObjectDestroyed(gameObject);


    }
}
