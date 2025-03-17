using UnityEngine;

namespace TicTacToe.General
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private bool _isOn;

        private Settings _settings;

        private void Start()
        {
            _settings = new Settings();
            _isOn = _settings.IsSoundEffectsEnabled();

            Settings.OnSoundEffectsChange += OnSoundEffectsToggleChange;
        }

        private void OnDestroy()
        {
            Settings.OnSoundEffectsChange -= OnSoundEffectsToggleChange;
        }

        private void OnSoundEffectsToggleChange(bool obj)
        {
            _isOn = _settings.IsSoundEffectsEnabled();
        }

        public void PlaySound()
        {
            if (_isOn) audioSource.Play();
        }
    }
}