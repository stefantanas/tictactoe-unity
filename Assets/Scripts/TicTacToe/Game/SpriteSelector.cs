using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Game
{
    public class SpriteSelector : MonoBehaviour
    {
        [SerializeField] private List<Image> xSpriteOptions;
        [SerializeField] private List<Image> oSpriteOptions;
        [SerializeField] private float fadeDuration = 0.1f;
        [SerializeField] private float scaleFactor = 1.2f;
        public static Sprite SelectedXSprite { get; private set; }
        public static Sprite SelectedOSprite { get; private set; }

        private void Start()
        {
            // Preselect first sprite in each category
            if (xSpriteOptions.Count > 0) SelectXSprite(xSpriteOptions[0].sprite, xSpriteOptions[0]);
            if (oSpriteOptions.Count > 0) SelectOSprite(oSpriteOptions[0].sprite, oSpriteOptions[0]);
        }

        public void SelectXButtonClicked(Image buttonImage)
        {
            SelectXSprite(buttonImage.sprite, buttonImage);
        }

        public void SelectOButtonClicked(Image buttonImage)
        {
            SelectOSprite(buttonImage.sprite, buttonImage);
        }

        private void SelectXSprite(Sprite sprite, Image buttonImage)
        {
            SelectedXSprite = sprite;
            StartCoroutine(UpdateSelectionVisuals(xSpriteOptions, buttonImage));
        }

        private void SelectOSprite(Sprite sprite, Image buttonImage)
        {
            SelectedOSprite = sprite;
            StartCoroutine(UpdateSelectionVisuals(oSpriteOptions, buttonImage));
        }

        private IEnumerator UpdateSelectionVisuals(List<Image> spriteOptions, Image selectedImage)
        {
            foreach (var img in spriteOptions)
            {
                var isSelected = img == selectedImage;
                StartCoroutine(FadeSprite(img, isSelected ? 1f : 0.5f));
                StartCoroutine(ScaleSprite(img.transform, isSelected ? scaleFactor : 1f));
            }

            yield return null;
        }

        private IEnumerator FadeSprite(Image img, float targetAlpha)
        {
            var startAlpha = img.color.a;
            var elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                var newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
                var color = img.color;
                color.a = newAlpha;
                img.color = color;
                yield return null;
            }

            var finalColor = img.color;
            finalColor.a = targetAlpha;
            img.color = finalColor;
        }

        private IEnumerator ScaleSprite(Transform spriteTransform, float targetScale)
        {
            var startScale = spriteTransform.localScale.x;
            var elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                var newScale = Mathf.Lerp(startScale, targetScale, elapsed / fadeDuration);
                spriteTransform.localScale = new Vector3(newScale, newScale, 1f);
                yield return null;
            }

            spriteTransform.localScale = new Vector3(targetScale, targetScale, 1f);
        }
    }
}