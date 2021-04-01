using System;
using System.Collections;
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

    [SerializeField] private RectTransform _canvas;
    [SerializeField] private GameObject _crosshair;

    private CanvasGroup _sliderCanvas;

    private void Start()
    {
        _sliderCanvas = GetComponent<CanvasGroup>();
        _slider = GetComponent<Slider>();
        _blackFade.alpha = 0.0f;
        _deathText.alpha = 0.0f;
        _subText.alpha = 0.0f;
        _damageScreen.alpha = 0.0f;
        _sliderCanvas.alpha = 0.0f;
    }

    private void LateUpdate()
    {
        if (Observer.Player == null)
            return;

        _canvas.position = Observer.PlayerHead.transform.position + Observer.PlayerHead.transform.forward * 55;
        _canvas.rotation = Observer.PlayerHead.transform.rotation;

        if (SceneSettings.Instance.VREnabled)
            _crosshair.SetActive(false);
        else
            _crosshair.SetActive(true);

        if (!SceneSettings.Instance.PlayerCanDie)
        {
            _blackFade.alpha = 0.0f;
            _deathText.alpha = 0.0f;
            _subText.alpha = 0.0f;
            _damageScreen.alpha = 0.0f;
            _sliderCanvas.alpha = 0.0f;
            return;
        }

        _sliderCanvas.alpha = 1.0f;
        _slider.value = Observer.LoudnessValue;
        _fillImage.color = Color.Lerp(_lowColor, _highColor, _slider.value);

        if (Observer.LoudnessValue > 0.50f)
        {
            _damageScreen.alpha = (Observer.LoudnessValue - 0.50f) / (1 - 0.50f);
        }
        else
        {
            _damageScreen.alpha = 0.0f;
        }


        if (Observer.LoudnessValue >= 1.0f && SceneSettings.Instance.PlayerCanDie)
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