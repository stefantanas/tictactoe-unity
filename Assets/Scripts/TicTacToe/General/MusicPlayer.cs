using UnityEngine;

namespace TicTacToe.General
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private Settings _settings;

        private static MusicPlayer Instance { get; set; }

        private void Awake()
        {
            // Singleton pattern to prevent music interruptions when scene changes
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.loop = true;
                audioSource.playOnAwake = false;
            }

            Settings.OnMusicChange += OnMusicToggleChange;
        }

        private void Start()
        {
            _settings = new Settings();

            var isOn = _settings.IsMusicEnabled();
            PlayMusic(isOn);
        }

        private void OnDestroy()
        {
            Settings.OnMusicChange -= OnMusicToggleChange;
        }

        private void OnMusicToggleChange(bool isOn)
        {
            PlayMusic(isOn);
        }

        private void PlayMusic(bool isOn)
        {
            if (isOn)
            {
                if (!audioSource.isPlaying) audioSource.Play();
            }
            else
            {
                audioSource.Pause();
            }
        }
    }
}