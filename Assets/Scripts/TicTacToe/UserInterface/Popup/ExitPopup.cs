using TicTacToe.General;

namespace TicTacToe.UserInterface.Popup
{
    public class ExitPopup : BasePopup
    {
        public void ExitGame()
        {
            CustomSceneManager.Instance.ExitGame();
        }
    }
}