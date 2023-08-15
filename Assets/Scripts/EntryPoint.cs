using UnityEngine;
using UnityEngine.Serialization;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PlayerController playerController;
    [FormerlySerializedAs("backgroundController")] [SerializeField] private EnvironmentController environmentController;
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
        playerController.Initialize(audioManager, inputManager, eventManager, pauseService);
        eventManager.Initialize(enemySpawner, environmentController, playerController);
        pauseService.Initialize(inputManager, menuManager.PauseWindow);
    }


    public void Start()
    {
        environmentController.needMove = true;
    }
}