using System;

namespace TicTacToe.General
{
    public class Settings
    {
        private readonly PlayerPrefsManager _playerPrefsManager = new();
        private const string SoundKey = "SoundEffectsEnabled";
        private const string MusicKey = "MusicEnabled";

        public Settings()
        {
            SetDefaultSettings();
        }

        public static event Action<bool> OnSoundEffectsChange;
        public static event Action<bool> OnMusicChange;

        public bool IsSoundEffectsEnabled()
        {
            return _playerPrefsManager.Load<bool>(SoundKey);
        }

        public bool IsMusicEnabled()
        {
            return _playerPrefsManager.Load<bool>(MusicKey);
        }

        public void SetSoundEffectsEnabled(bool enabled)
        {
            _playerPrefsManager.Save(SoundKey, enabled);
            TriggerSoundEffectsChangeEvent(enabled);
        }

        public void SetMusicEnabled(bool enabled)
        {
            _playerPrefsManager.Save(MusicKey, enabled);
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
            _playerPrefsManager.PrepopulatePlayerPrefs(SoundKey, true);
            _playerPrefsManager.PrepopulatePlayerPrefs(MusicKey, true);
        }
    }
}