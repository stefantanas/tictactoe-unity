using System.Collections;
using UnityEngine;

namespace TicTacToe.UserInterface.Popup
{
    public class BasePopup : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.1f;
        [SerializeField] private Vector3 startScale = new(0.1f, 0.1f, 1f);
        protected CanvasGroup PopupCanvasGroup;

        protected virtual void Start()
        {
            PopupCanvasGroup = GetComponent<CanvasGroup>();
            transform.localScale = startScale;
        }

        public void OpenPopup()
        {
            if (PopupCanvasGroup != null)
            {
                PopupCanvasGroup.blocksRaycasts = true;
                PopupCanvasGroup.interactable = true;
                StopAllCoroutines();
                StartCoroutine(AnimatePopup(1f, Vector3.one));
            }
        }

        public void ClosePopup()
        {
            if (PopupCanvasGroup != null)
            {
                PopupCanvasGroup.interactable = false;
                PopupCanvasGroup.blocksRaycasts = false;
                StopAllCoroutines();
                StartCoroutine(AnimatePopup(0f, startScale));
            }
        }

        private IEnumerator AnimatePopup(float targetAlpha, Vector3 targetScale)
        {
            var elapsedTime = 0f;
            var startAlpha = PopupCanvasGroup.alpha;
            var startScaleLocal = transform.localScale;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                var t = elapsedTime / fadeDuration;
                PopupCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                transform.localScale = Vector3.Lerp(startScaleLocal, targetScale, t);
                yield return null;
            }

            PopupCanvasGroup.alpha = targetAlpha;
            transform.localScale = targetScale;
        }
    }
}