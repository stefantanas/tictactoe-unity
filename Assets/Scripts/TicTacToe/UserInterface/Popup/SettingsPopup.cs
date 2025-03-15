
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UserInterface.Popup
{
    public class SettingsPopup : BasePopup
    {
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Toggle _musicToggle;

        protected override void Start()
        {
            
            base.Start();
        }
    }
}