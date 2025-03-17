using TicTacToe.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.General
{
    public class CustomSceneManager : MonoBehaviour
    {
        public static CustomSceneManager Instance { get; private set; }

        public Sprite SelectedXSprite { get; private set; }
        public Sprite SelectedOSprite { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SwitchToGameScene()
        {
            SetSelectedSprites(SpriteSelector.SelectedXSprite, SpriteSelector.SelectedOSprite);
            ChangeScene(SceneNames.Game);
        }

        public void SwitchToPlayScene()
        {
            ChangeScene(SceneNames.Play);
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        private void SetSelectedSprites(Sprite xSprite, Sprite oSprite)
        {
            SelectedXSprite = xSprite;
            SelectedOSprite = oSprite;
        }
    }
}