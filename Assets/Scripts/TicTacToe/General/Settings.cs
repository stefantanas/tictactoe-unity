using System;

namespace TicTacToe.General
{
    public class Settings
    {
        private readonly PlayerPrefsManager _playerPrefsManager = new();

        public Settings()
        {
            SetDefaultSettings();
        }

        public static event Action<bool> OnSoundEffectsChange;
        public static event Action<bool> OnMusicChange;

        public bool IsSoundEffectsEnabled()
        {
            return _playerPrefsManager.Load<bool>("SoundEffectsEnabled");
        }

        public bool IsMusicEnabled()
        {
            return _playerPrefsManager.Load<bool>("MusicEnabled");
        }

        public void SetSoundEffectsEnabled(bool enabled)
        {
            _playerPrefsManager.Save("SoundEffectsEnabled", enabled);
            TriggerSoundEffectsChangeEvent(enabled);
        }

        public void SetMusicEnabled(bool enabled)
        {
            _playerPrefsManager.Save("MusicEnabled", enabled);
            TriggerMusicChangeEvent(enabled);
        }

        private static void TriggerSoundEffectsChangeEvent(bool isOn)
        {
            OnSoundEffectsChange?.Invoke(isOn);
        }

        private static void TriggerMusicChangeEvent(bool isOn)
        {
            OnMusicChange?.Invoke(isOn);
        }

        private void SetDefaultSettings()
        {
            _playerPrefsManager.PrepopulatePlayerPrefs("SoundEffectsEnabled", true);
            _playerPrefsManager.PrepopulatePlayerPrefs("MusicEnabled", true);
        }
    }
}