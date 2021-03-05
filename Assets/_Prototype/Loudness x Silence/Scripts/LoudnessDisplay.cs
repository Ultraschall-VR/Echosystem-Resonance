﻿using System.Collections;
using Echosystem.Resonance.Prototyping;
using UnityEngine;
using UnityEngine.UI;

public class LoudnessDisplay : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Image _fillImage;

    [SerializeField] private Color _lowColor;
    [SerializeField] private Color _highColor;

    [SerializeField] private CanvasGroup _damageScreen;

    [SerializeField] private CanvasGroup _blackFade;
    [SerializeField] private CanvasGroup _deathText;
    [SerializeField] private CanvasGroup _subText;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _blackFade.alpha = 0.0f;
        _deathText.alpha = 0.0f;
        _subText.alpha = 0.0f;
        _damageScreen.alpha = 0.0f;
        
        if(!SceneSettings.Instance.PlayerCanDie) 
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!SceneSettings.Instance.PlayerCanDie) 
            return;
        
        
        if (_slider.value > 0.50f)
        {
            _damageScreen.alpha = _slider.value;
        }
        else
        {
            _damageScreen.alpha = 0.0f;
        }

        _slider.value = Observer.LoudnessValue;
        _fillImage.color = Color.Lerp(_lowColor, _highColor, _slider.value);

        if (_slider.value >= 1.0f && SceneSettings.Instance.PlayerCanDie)
        {
            StartCoroutine(FadeToBlack());
        }
    }

    private IEnumerator FadeToBlack()
    {
        Observer.IsRespawning = true;

        float t = 0.0f;
        float timer = SceneSettings.Instance.RespawnTime / 5;
        
        while (t < timer)
        {
            t += Time.deltaTime;

            _blackFade.alpha = Mathf.Lerp(0, 1, t / timer);
            yield return null;
        }
        
        t = 0.0f;
        
        while (t < timer)
        {
            t += Time.deltaTime;

            _deathText.alpha = Mathf.Lerp(0, 1, t / timer);
            yield return null;
        }
        
        t = 0.0f;
        
        while (t < timer)
        {
            t += Time.deltaTime;

            _subText.alpha = Mathf.Lerp(0, 1, t / timer);
            yield return null;
        }
        
        t = 0.0f;
        
        while (t < timer)
        {
            t += Time.deltaTime;

            _deathText.alpha = Mathf.Lerp(1, 0, t / timer);
            yield return null;
        }
        
        t = 0.0f;

        while (t < timer)
        {
            t += Time.deltaTime;

            _blackFade.alpha = Mathf.Lerp(1, 0, t / timer);
            _subText.alpha = Mathf.Lerp(1, 0, t / timer);
            yield return null;
        }
        
        Observer.IsRespawning = false;

        yield return null;
    }
}