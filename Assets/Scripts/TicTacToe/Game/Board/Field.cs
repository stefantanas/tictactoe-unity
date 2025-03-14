using System;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Game.Board
{
    public class Field : MonoBehaviour
    {
        public static event Action<int, int> OnFieldClicked;

        [SerializeField] private int x;
        [SerializeField] private int y;
        [SerializeField] private Image xSprite;
        [SerializeField] private Image oSprite;

        private bool _isMarked;

        private void Start()
        {
            ResetField();
        }

        public void SelectField()
        {
            if (_isMarked) return;

            OnFieldClicked?.Invoke(x, y);
        }

        public void SetMark(Mark mark)
        {
            if (_isMarked) return; // Prevent double marking

            _isMarked = true;
            if (mark == Mark.X)
                SetSpriteVisible(xSprite);
            else if (mark == Mark.O) SetSpriteVisible(oSprite);
        }

        private void SetSpriteVisible(Image sprite)
        {
            if (sprite == null) return;
            var color = sprite.color;
            color.a = 1f; // Make sprite fully visible
            sprite.color = color;
        }

        public void ResetField()
        {
            _isMarked = false;
            SetSpriteInvisible(xSprite);
            SetSpriteInvisible(oSprite);
        }

        private void SetSpriteInvisible(Image sprite)
        {
            if (sprite == null) return;
            var color = sprite.color;
            color.a = 0f; // Hide sprite
            sprite.color = color;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
    }
}