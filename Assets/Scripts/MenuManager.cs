using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private EntryPoint entryPoint;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Toggle toggleSfx;
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        Instance = this;

        playAgainButton.onClick.AddListener(() => entryPoint.RestartLevel());
        tryAgainButton.onClick.AddListener(() => entryPoint.RestartLevel());
        continueButton.onClick.AddListener(ContinueGame);
        toggleMusic.onValueChanged.AddListener(AudioManager.Instance.ToggleMusic);
        toggleSfx.onValueChanged.AddListener(AudioManager.Instance.ToggleSfx);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.MusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SfxVolume);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseWindow.activeSelf)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseWindow.SetActive(true);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
        pauseWindow.SetActive(false);
    }
}
