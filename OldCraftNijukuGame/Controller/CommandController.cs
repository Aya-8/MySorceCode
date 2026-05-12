using System.Collections.Generic;
using Kapuro2025Winter.Scripts.Stiring.Controller;
using Kapuro2025Winter.Scripts.Stiring.Director;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CommandController : MonoBehaviour
{
    [SerializeField] private PGDirector director;
    [SerializeField] private InputController inputController;
    
    private List<InputDirection> inputBuffer = new ();
    private float lastInputTime;
    private float limitTime = 1f;
    public InputCommand CurrentInputCommand { get; private set; }
    

    void Update()
    {
        CurrentInputCommand = InputCommand.None;
        if (director.CurrentPhase() == Phase.Start) return;
        
        // 最後の入力からの経過時間 
        float elapsedTime = Time.time - lastInputTime;
        if (inputBuffer.Count > 0 && elapsedTime > limitTime)
        {
            inputBuffer.Clear();
        }
        
        InputDirection direction = inputController.GetCurrentInputDirection();

        if (direction == InputDirection.None) return;
        
        TryAddToBuffer(direction);
    }

    private void TryAddToBuffer(InputDirection direction)
    {

        // 最後に記録した方向キーと入力が同じ　なら返る
        if (inputBuffer.Count > 0 && inputBuffer[^1] == direction) return;
        
        inputBuffer.Add(direction);
        lastInputTime = Time.time;

        CheckDirectionToCommand();
    }
    

    private void CheckDirectionToCommand()
    {
        // 右回し： → ↓ ← ↑
        if (MatchLast(InputDirection.Right, InputDirection.Down, InputDirection.Left, InputDirection.Up))
        {
            CurrentInputCommand = InputCommand.Right;
            inputBuffer.Clear();
            return;
        }

        //　左回し： ← ↓ → ↑
        if (MatchLast(InputDirection.Left, InputDirection.Down, InputDirection.Right, InputDirection.Up))
        {
            CurrentInputCommand = InputCommand.Left;
            inputBuffer.Clear();
            return;
        }
        
    }
       
    

    private bool MatchLast(params InputDirection[] directions)
    {
        if (inputBuffer.Count < directions.Length) return false;

        int start = inputBuffer.Count - directions.Length;

        for (int i = 0; i < directions.Length; i++)
        {
            if (inputBuffer[start + i] != directions[i]) return false;
        }

        return true;
    }

    



}