using System;
using TicTacToe.General;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Game.Board
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private int x;
        [SerializeField] private int y;
        [SerializeField] private Image xSprite;
        [SerializeField] private Image oSprite;

        private bool _isMarked;

        private void Start()
        {
            if (CustomSceneManager.Instance.SelectedXSprite != null)
                xSprite.sprite = CustomSceneManager.Instance.SelectedXSprite;

            if (CustomSceneManager.Instance.SelectedOSprite != null)
                oSprite.sprite = CustomSceneManager.Instance.SelectedOSprite;

            ResetField();
        }

        public static event Action<int, int> OnFieldClicked;

        public void SelectField()
        {
            if (_isMarked) return;
            OnFieldClicked?.Invoke(x, y);
        }

        public void ResetField()
        {
            _isMarked = false;
            SetSpriteInvisible(xSprite);
            SetSpriteInvisible(oSprite);
        }

        public void SetMark(Mark mark)
        {
            if (_isMarked) return;

            _isMarked = true;
            if (mark == Mark.X)
                SetSpriteVisible(xSprite);
            else if (mark == Mark.O)
                SetSpriteVisible(oSprite);
        }

        private void SetSpriteVisible(Image sprite)
        {
            if (sprite == null) return;
            var color = sprite.color;
            color.a = 1f;
            sprite.color = color;
        }

        private void SetSpriteInvisible(Image sprite)
        {
            if (sprite == null) return;
            var color = sprite.color;
            color.a = 0f;
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