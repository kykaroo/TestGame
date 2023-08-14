using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    
    private EventManager eventManager;
    
    
    public void Initialize(EventManager manager)
    {
        manager.OnVictory += () => gameObject.SetActive(true);
        manager.OnRestart += () => gameObject.SetActive(false);
        
        playAgainButton.onClick.AddListener(manager.RestartLevel);
    }
}
