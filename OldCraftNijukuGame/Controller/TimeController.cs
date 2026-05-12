using Kapuro2025Winter.Scripts.Stiring.Director;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private float timer;
    private int RightDuration = 10;
    private int LeftDuration = 20;
    private int BothDuration = 30;
    private bool isRunningGame = false;
    
    [SerializeField] private PGDirector pgDirector;

    private Phase? lastPhase = Phase.Right;
    public event System.Action<Phase> OnPhaseTimeReached;

    public void StartTimer()
    {
        timer = 0;
        isRunningGame = true;
        lastPhase = Phase.Right;
        ApplyPhase(Phase.Right);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isRunningGame)
        { 
            return;
        }
        timer += Time.deltaTime;

        Phase next;
        if (timer <= RightDuration)
        {
            next = Phase.Right;
        }
        else if (timer <= LeftDuration)
        {
            next = Phase.Left; 
        }
        else if (timer <= BothDuration)
        {
            next = Phase.Both;
        }
        else
        {
            next = Phase.End;
        }

        ApplyPhase(next);

        if (next == Phase.End)
        {
            isRunningGame = false;
            timer = BothDuration;
        }
    }

    private void ApplyPhase(Phase phase)
    {
        Debug.Log("Applying phase: " + phase);
        if (lastPhase.HasValue && lastPhase.Value == phase) return;
        lastPhase = phase;
        OnPhaseTimeReached?.Invoke(phase);
    }
    
}