using System;
using UnityEngine;
using System.Collections;
using Kapuro2025Winter.Scripts.Stiring.Director;
using TMPro;


public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject exPanel;
    [SerializeField] private PGDirector pgDirector;
    [SerializeField] private GameObject finPanel;

    [Header("イベント発火の間の時間")]
    [SerializeField] private float explainGameTitleDuration = 3f;
    [SerializeField] private float explainMixPotDuration = 3f;
    [SerializeField] private float explainSodaDuration = 5f;
    [SerializeField] private float enterPowderDuration = 2f;
    
    [SerializeField] private float countdownInterval = 1f;
    [SerializeField] private float hideCountdownDelay = 0.5f;
    
    private Coroutine countdownRoutine;

    public event Action OnFinishedSequence;
    public event Action OnCountdownStarted;

    public event Action OnEnterPowder;
    public event Action OnExplainGame;
    public event Action OnExplainSoda;
    public event Action OnMixNabe;

    public event Action<StartSequence> OnStartSequenceChanged;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exPanel.SetActive(true);
        finPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (pgDirector != null)
            pgDirector.OnChangePhase += OnChangeUI;
    }

    private void OnDisable()
    {
        if (pgDirector != null)
            pgDirector.OnChangePhase -= OnChangeUI;
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);
        countdownRoutine = null;
    }

    private void OnChangeUI(Phase phase)
    {
        switch (phase)
        {
            case Phase.Start: StartCountDown(); break;
        }
    }


    private void StartCountDown()
    {

        if (countdownRoutine != null) StopCoroutine(countdownRoutine);
        countdownRoutine = StartCoroutine(PlaySequences());
    }

    private IEnumerator PlaySequences()
    {
        yield return PlayIntroSequence();
        yield return PlayCountDownSequence();
    }

    private IEnumerator PlayIntroSequence()
    {
        //OnExplainGame?.Invoke();
        OnStartSequenceChanged?.Invoke(StartSequence.ExplainGame);
        yield return new WaitForSecondsRealtime(explainGameTitleDuration);
        
        //OnMixNabe?.Invoke();
        OnStartSequenceChanged?.Invoke(StartSequence.MixPot);
        yield return new WaitForSecondsRealtime(explainMixPotDuration);
        exPanel.SetActive(false);
        
        OnEnterPowder?.Invoke();
        OnStartSequenceChanged?.Invoke(StartSequence.EnterPowder);
        yield return new WaitForSecondsRealtime(enterPowderDuration);
        
        //OnExplainSoda?.Invoke();
        OnStartSequenceChanged?.Invoke(StartSequence.ExplainSoda);
        yield return new WaitForSecondsRealtime(explainSodaDuration);
    }

    private IEnumerator PlayCountDownSequence()
    {
        //OnCountdownStarted?.Invoke();
        OnStartSequenceChanged?.Invoke(StartSequence.Countdown);
        Debug.Log("Countdown startedイベントを発火したはず");
        for (int i = 3; i >= 1; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSecondsRealtime(countdownInterval);
        }

        text.text = ("Start");

        OnFinishedSequence?.Invoke();
        yield return new WaitForSecondsRealtime(hideCountdownDelay);
        text.enabled = false;
    }

    
}
    

