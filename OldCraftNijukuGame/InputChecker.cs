using Kapuro2025Winter.Scripts.Stiring.Controller;
using Kapuro2025Winter.Scripts.Stiring.Director;
using Kapuro2025Winter.Scripts.Stiring.Pole;
using UnityEngine; 
public class InputChecker : MonoBehaviour
{
    [SerializeField] private PGPlayerController playerController;
    [SerializeField] private PGDirector director; 
    [SerializeField] private PoleMoveAnimation  poleMoveAnimation;
    
    public event System.Action  OnCorrectKeyPressed;
    public event System.Action  OnWrongKeyPressed;
    void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (playerController.CurrentInput == InputCommand.None | director.CurrentPhase()== Phase.Start) return;
        if (director.CurrentPhase() == Phase.End) return;
        if(director.CurrentPhase() == Phase.Right && playerController.CurrentInput == InputCommand.Right) OnCorrectKeyPressed?.Invoke();
        else if(director.CurrentPhase() == Phase.Left && playerController.CurrentInput == InputCommand.Left) OnCorrectKeyPressed?.Invoke();
        else if (director.CurrentPhase() == Phase.Both) OnCorrectKeyPressed?.Invoke();
        else OnWrongKeyPressed?.Invoke();
        
    }
}