using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.General
{
    public class SceneSwitcher : MonoBehaviour
    {
        public void ChangeScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}