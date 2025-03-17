using TicTacToe.General;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UserInterface.Popup
{
    public class SettingsPopup : BasePopup
    {
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle musicToggle;

        private Settings _settings;

        protected override void Start()
        {
            _settings = new Settings();

            soundToggle.isOn = _settings.IsSoundEffectsEnabled();
            musicToggle.isOn = _settings.IsMusicEnabled();

            soundToggle.onValueChanged.AddListener(SoundEffectsToggleOnValueChanged);
            musicToggle.onValueChanged.AddListener(MusicToggleOnValueChanged);
            base.Start();
        }

        private void SoundEffectsToggleOnValueChanged(bool isOn)
        {
            _settings.SetSoundEffectsEnabled(isOn);
        }

        private void MusicToggleOnValueChanged(bool isOn)
        {
            _settings.SetMusicEnabled(isOn);
        }
    }
}