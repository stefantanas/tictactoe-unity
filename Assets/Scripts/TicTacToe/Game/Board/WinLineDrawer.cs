using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Game.Board
{
    public class WinLineDrawer : MonoBehaviour
    {
        [SerializeField] private Image winLine;
        [SerializeField] private RectTransform[] boardFields;
        [SerializeField] private float lineThickness = 10f;

        public void DrawWinLine((int, int)[] winLinePositions)
        {
            // Identify the first and last cells in the winning line to get start/end transforms.
            var startRect = boardFields[winLinePositions[0].Item1 * 3 + winLinePositions[0].Item2];
            var endRect = boardFields[winLinePositions[2].Item1 * 3 + winLinePositions[2].Item2];

            // anchoredPosition is used since these elements share the same parent.
            var startPos = startRect.anchoredPosition;
            var endPos = endRect.anchoredPosition;

            // Calculate distance and angle for the line.
            var diff = endPos - startPos;
            var distance = diff.magnitude;
            var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            // Position and rotate the line at the start cell.
            var lineRect = winLine.rectTransform;
            lineRect.anchoredPosition = startPos;
            lineRect.localEulerAngles = new Vector3(0, 0, angle);
            lineRect.sizeDelta = new Vector2(0f, lineThickness);

            winLine.gameObject.SetActive(true);
            StartCoroutine(AnimateWinLine(lineRect, distance, 0.5f));
        }

        private IEnumerator AnimateWinLine(RectTransform lineRect, float targetWidth, float duration)
        {
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                var currentWidth = Mathf.Lerp(0f, targetWidth, t);
                lineRect.sizeDelta = new Vector2(currentWidth, lineThickness);

                yield return null;
            }

            lineRect.sizeDelta = new Vector2(targetWidth, lineThickness);
        }

        public void ClearWinLine()
        {
            winLine.gameObject.SetActive(false);
        }
    }
}