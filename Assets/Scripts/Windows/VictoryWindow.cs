using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;

    private bool selfActiveBuffer;
    
    public void Initialize(EventManager manager, PauseService pause)
    {
        manager.OnVictory += OpenWindow;
        manager.OnRestart += CloseWindow;
        pause.OnPause += OnPauseWindowClose;
        
        playAgainButton.onClick.AddListener(manager.RestartLevel);
    }
    
    private void OnPauseWindowClose(bool onPause)
    {
        if (onPause)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(selfActiveBuffer);
    }

    private void OpenWindow()
    {
        selfActiveBuffer = true;
        gameObject.SetActive(true);
    }
    
    private void CloseWindow()
    {
        selfActiveBuffer = false;
        gameObject.SetActive(false);
    }
}
