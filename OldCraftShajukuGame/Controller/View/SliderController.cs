using Kapuro2025Winter.Scripts.Stiring.Director;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private InputChecker inputChecker;
    [SerializeField] private PGDirector pgdirector;
    private Slider slider;
    
    private bool isFinished = false;
    private bool isStarted = false;
    private float add;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 100;
        slider.enabled = false;
    }

    void OnEnable()
    {
        inputChecker.OnCorrectKeyPressed += AddSliderValue;
        pgdirector.OnChangePhase += FinishSliderValue;
        pgdirector.OnChangePhase += ActivateSlider;
        
    }

    void OnDisable()
    {
        inputChecker.OnCorrectKeyPressed -= AddSliderValue;
        pgdirector.OnChangePhase -= FinishSliderValue;
        pgdirector.OnChangePhase -= ActivateSlider;
    }

    private void ActivateSlider(Phase phase)
    {
        Debug.Log("Activate Sliderが呼ばれてる");
        if (phase == Phase.Right)
        {
            Debug.Log("Activate Sliderが呼ばれてさらにSetActiveまで進んでいる");
            slider.gameObject.SetActive(true);
            isStarted = true;
        }
    }

    private void AddSliderValue()
    {
        if (!isStarted) return;
        slider.value += 1;
    }

    private void FinishSliderValue(Phase phase)
    {
        if (phase != Phase.End) return;
        isFinished = true;
        add = slider.value;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (!isFinished) return;
        
        slider.value = Mathf.MoveTowards(slider.value, slider.maxValue, Time.deltaTime * 70);
        
        if (slider.value >= slider.maxValue)
        {
            isFinished = false;
            slider.enabled = false;
        }
        
    }
}
