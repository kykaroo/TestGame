using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Toggle toggleSfx;
    [SerializeField] private Button continueButton;
    private AudioManager audioManager;

    public event Action OnContinueGame;
    
    public void Initialize(AudioManager manager, PauseService pause)
    {
        audioManager = manager;
        
        continueButton.onClick.AddListener(() => OnContinueGame?.Invoke());
        toggleMusic.onValueChanged.AddListener(audioManager.ToggleMusic);
        toggleSfx.onValueChanged.AddListener(audioManager.ToggleSfx);
        musicSlider.onValueChanged.AddListener(audioManager.MusicVolume);
        sfxSlider.onValueChanged.AddListener(audioManager.SfxVolume);
        pause.OnPause += gameObject.SetActive;
    }
}
