using UnityEngine;

namespace TicTacToe.UserInterface.Popup
{
    public class BasePopup : MonoBehaviour
    {
        protected CanvasGroup PopupCanvasGroup;

        protected virtual void Start()
        {
            PopupCanvasGroup = GetComponent<CanvasGroup>();
        }

        public void OpenPopup()
        {
            if (PopupCanvasGroup != null)
            {
                PopupCanvasGroup.alpha = 1;
                PopupCanvasGroup.blocksRaycasts = true;
                PopupCanvasGroup.interactable = true;
            }
        }

        public void ClosePopup()
        {
            if (PopupCanvasGroup != null)
            {
                PopupCanvasGroup.alpha = 0;
                PopupCanvasGroup.interactable = false;
                PopupCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}