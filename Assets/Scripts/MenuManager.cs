using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private PauseWindow pauseWindow;
    [SerializeField] private GameOverWindow gameOverWindow;
    [SerializeField] private VictoryWindow victoryWindow;

    public PauseWindow PauseWindow => pauseWindow; 

    
    private bool onVictoryOrDefeatScreen;
    

    public void Initialize(AudioManager audio, EventManager manager, PauseService service)
    {
        pauseWindow.Initialize(audio, service);
        gameOverWindow.Initialize(manager);
        victoryWindow.Initialize(manager);
    }
}
