using System;
using UnityEngine;

public class PauseService
{
    private InputManager inputManager;
    
    private bool onPause;

    public event Action<bool> OnPause;
    
    public void Initialize(InputManager input, PauseWindow pause)
    {
        inputManager = input;

        pause.OnContinueGame += ContinueGame;
        inputManager.OnEscapeButtonClicked += PauseCheck;
    }
    
    private void PauseCheck()
    {
        if (onPause)
        {
            ContinueGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        onPause = true;
        OnPause?.Invoke(onPause);
        Time.timeScale = 0f;
    }

    private void ContinueGame()
    {
        onPause = false;
        OnPause?.Invoke(onPause);
        Time.timeScale = 1f;
    }
}
