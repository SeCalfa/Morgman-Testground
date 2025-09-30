using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Code.Logic.NativeLogicOverrides.StaticItems.Flashlight
{
    public class FlashlightPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI batteriesCountText;
        [SerializeField] private Image fill;
        
        private CanvasGroup _canvasGroup;
        private Coroutine _coroutine;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
        }

        public void UpdateProgress(float current, float max)
        {
            var fillAmount = current / max;
            fill.fillAmount = fillAmount;
        }

        public void UpdateBatteries(int count)
        {
            batteriesCountText.text = count.ToString();
        }

        public void Show()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            
            _coroutine = StartCoroutine(ShowCoroutine());
        }
        
        public void Hide()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            
            _coroutine = StartCoroutine(HideCoroutine());
        }

        private IEnumerator ShowCoroutine()
        {
            while (_canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += 0.05f;
                
                yield return new WaitForSeconds(0.01f);
            }
        }

        private IEnumerator HideCoroutine()
        {
            while (_canvasGroup.alpha > 0f)
            {
                _canvasGroup.alpha -= 0.05f;
                
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
