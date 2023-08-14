using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;
 
    private EventManager eventManager;
    private PlayerController playerController;

    public void Initialize(EventManager manager)
    {
        manager.OnDefeat += () => gameObject.SetActive(true);
        manager.OnRestart += () => gameObject.SetActive(false);

        tryAgainButton.onClick.AddListener(manager.RestartLevel);
    }
}
