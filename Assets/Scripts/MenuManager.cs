using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private EntryPoint entryPoint;
    

    private void Awake()
    {
        playAgainButton.onClick.AddListener(() => entryPoint.RestartLevel());
        tryAgainButton.onClick.AddListener(() => entryPoint.RestartLevel());
    }
}
