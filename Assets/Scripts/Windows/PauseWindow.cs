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

    public event Action OnContinueGame;
    
    public void Initialize(AudioManager manager, PauseService pause)
    {
        continueButton.onClick.AddListener(() => OnContinueGame?.Invoke());
        toggleMusic.onValueChanged.AddListener(manager.ToggleMusic);
        toggleSfx.onValueChanged.AddListener(manager.ToggleSfx);
        musicSlider.onValueChanged.AddListener(manager.MusicVolume);
        sfxSlider.onValueChanged.AddListener(manager.SfxVolume);
        pause.OnPause += gameObject.SetActive;
    }
}
