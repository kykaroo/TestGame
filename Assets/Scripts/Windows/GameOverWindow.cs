using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;
 
    private EventManager eventManager;
    private PlayerController playerController;
    private PauseService pauseService;
    
    private bool selfActiveBuffer;

    public void Initialize(EventManager manager, PauseService pause)
    {
        pauseService = pause;
        
        manager.OnDefeat += OpenWindow;
        manager.OnRestart += CloseWindow;
        pauseService.OnPause += OnPauseWindowClose;

        tryAgainButton.onClick.AddListener(manager.RestartLevel);
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
