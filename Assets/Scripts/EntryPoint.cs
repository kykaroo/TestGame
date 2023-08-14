using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BackgroundController backgroundController;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private EnemySpawner enemySpawner;
    
    private EventManager eventManager;
    private PauseService pauseService;
    
    private void Awake()
    {
        eventManager = new();
        pauseService = new();
        
        menuManager.Initialize(audioManager, eventManager, pauseService);
        enemySpawner.Initialize(audioManager);
        playerController.Initialize(menuManager, audioManager, inputManager, eventManager, pauseService);
        eventManager.Initialize(enemySpawner, backgroundController, playerController);
        pauseService.Initialize(inputManager, menuManager.PauseWindow);
    }


    public void Start()
    {
        backgroundController.needMove = true;
    }
}