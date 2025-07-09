using System;
using Runtime.Gameplay.HelperServices.UserData.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Gameplay.ScreensPopups.Screen
{
    public class SettingsScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        
        public event Action<float> OnMusicVolumeChanged;
        public event Action<float> OnSoundVolumeChanged;
        
        public event Action OnBackPressed;

        public void Initialize(SettingsData settings)
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());

            _musicSlider.value = settings.MusicVolume;
            _soundSlider.value = settings.SoundVolume;
            
            _musicSlider.onValueChanged.AddListener((value) => OnMusicVolumeChanged?.Invoke(value));
            _soundSlider.onValueChanged.AddListener((value) => OnSoundVolumeChanged?.Invoke(value));
        } 
    }
}