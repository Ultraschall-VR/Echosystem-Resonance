using System.Collections;
using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private List<Canvas> _canvases;
        [SerializeField] private float _delayTime;
        [SerializeField] private float _fadeOutTime;
        [SerializeField] private float _fadeInTime;

        [SerializeField] private CanvasGroup _canvasGroup;

        private bool _isFading;

        public static UiManager Instance;
        
        #region UnityEvents
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            _canvasGroup.alpha = 0f;
            HideAll();
            Invoke("Delay", _delayTime);
            FadeIn();
        }

        private void Update()
        {
            Debug.Log("Is Fading: " + _isFading);
            
            if (Observer.SilenceSphereExited)
            {
                LoadCanvas(1);
            }

            if (Observer.CollectedObjects == Observer.MaxCollectibleObjects)
            {
                FadeOut();
            }
        }

        #endregion

        private void Delay()
        {
            _canvases[0].enabled = true;
        }

        public void HideAll()
        {
            foreach (var canvas in _canvases)
            {
                canvas.enabled = false;
            }
        }

        public void FadeOut()
        {
            if(_isFading)
                return;
            
            StartCoroutine(Fade(true));
        }

        public void FadeIn()
        {
            if (_isFading)
                return;

            StartCoroutine(Fade(false));
        }

        public void LoadCanvas(int index)
        {
            HideAll();
            _canvases[index].enabled = true;
        }

        private IEnumerator Fade(bool isFadeOut)
        {
            if(isFadeOut)
                yield return new WaitForSeconds(2f);
            
            float timer;
            float t = 0.0f;
            float currentAlpha = _canvasGroup.alpha;
            float targetAlpha;
            
            if (isFadeOut)
            {
                timer = _fadeOutTime;
                targetAlpha = 0f;
            }
            
            else
            {
                timer = _fadeInTime;
                targetAlpha = 1f;
            }

            while (t < timer)
            {
                _isFading = true;
                t += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(currentAlpha, targetAlpha, t / timer);
                yield return null;
            }

            _isFading = false;
            
            yield return null;
        }
    }
}

