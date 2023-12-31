using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private PauseWindow pauseWindow;
    [SerializeField] private GameOverWindow gameOverWindow;
    [SerializeField] private VictoryWindow victoryWindow;

    public PauseWindow PauseWindow => pauseWindow; 

    
    private bool onVictoryOrDefeatScreen;
    

    public void Initialize(AudioManager audio, EventManager manager, PauseService pause)
    {
        pauseWindow.Initialize(audio, pause);
        gameOverWindow.Initialize(manager, pause);
        victoryWindow.Initialize(manager, pause);
    }
}
