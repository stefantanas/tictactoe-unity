using TicTacToe.General;

namespace TicTacToe.UserInterface.Popup
{
    public class PlayPopup : BasePopup
    {
        public void StartGame()
        {
            CustomSceneManager.Instance.SwitchToGameScene();
        }
    }
}