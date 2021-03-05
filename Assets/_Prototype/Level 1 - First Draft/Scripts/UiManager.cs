using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class UiManager : MonoBehaviour
    {
        private List<Canvas> _canvases;
        [SerializeField] private float _delayTime;
        [SerializeField] private float _fadeOutTime;
        [SerializeField] private float _fadeInTime;

        private CanvasGroup _canvasGroup;

        [SerializeField] private bool _firstCanvasOnStart;

        private bool _isFading;

        #region UnityEvents

        private void Awake()
        {
            
        }

        private void Start()
        {
            _canvases = new List<Canvas>();
            
            foreach (Transform child in this.transform)
            {
                if (child.GetComponent<Canvas>())
                {
                    _canvases.Add(child.GetComponent<Canvas>());
                }
            }

            _canvasGroup = GetComponent<CanvasGroup>();
            
            _canvasGroup.alpha = 0f;
            HideAll();
            
            if (_firstCanvasOnStart)
            {
                Invoke("Delay", _delayTime);
                FadeIn();
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

