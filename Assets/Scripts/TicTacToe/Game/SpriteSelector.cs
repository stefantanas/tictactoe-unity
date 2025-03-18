using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Game
{
    /// <summary>
    ///     Allows the player to choose custom X and O sprites from a list of UI Images.
    ///     Also handles visual feedback (fading/scaling) to indicate which sprite is selected.
    /// </summary>
    public class SpriteSelector : MonoBehaviour
    {
        [SerializeField] private List<Image> xSpriteOptions;
        [SerializeField] private List<Image> oSpriteOptions;

        [SerializeField] private float fadeDuration = 0.1f;
        [SerializeField] private float scaleFactor = 1.2f;

        // Stores the currently selected X and O sprites, accessible by other classes.
        public static Sprite SelectedXSprite { get; private set; }
        public static Sprite SelectedOSprite { get; private set; }

        private void Start()
        {
            // Automatically select the first sprite in each list (if available) to provide a default choice.
            if (xSpriteOptions.Count > 0)
                SelectXSprite(xSpriteOptions[0].sprite, xSpriteOptions[0]);

            if (oSpriteOptions.Count > 0)
                SelectOSprite(oSpriteOptions[0].sprite, oSpriteOptions[0]);
        }

        // Called when the player clicks on an X-sprite button.
        public void SelectXButtonClicked(Image buttonImage)
        {
            SelectXSprite(buttonImage.sprite, buttonImage);
        }

        // Called when the player clicks on an O-sprite button.
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

        /// <summary>
        ///     Highlights the chosen sprite in the list by making it fully opaque and scaled up,
        ///     while dimming/scaling down the other sprites.
        /// </summary>
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

        /// <summary>
        ///     Smoothly transitions an Image's alpha from its current value to targetAlpha.
        /// </summary>
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

            // Ensure final alpha is exactly the target.
            var finalColor = img.color;
            finalColor.a = targetAlpha;
            img.color = finalColor;
        }

        /// <summary>
        ///     Smoothly scales a Transform from its current local scale to the specified target scale.
        /// </summary>
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

            // Ensure final scale is exactly the target.
            spriteTransform.localScale = new Vector3(targetScale, targetScale, 1f);
        }
    }
}