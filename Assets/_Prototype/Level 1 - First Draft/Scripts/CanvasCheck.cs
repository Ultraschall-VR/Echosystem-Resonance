using System.Collections;
using System.Collections.Generic;
using Echosystem.Resonance.UI;
using UnityEngine;

public class CanvasCheck : MonoBehaviour
{
    private Canvas _canvas;
    private bool _isEnabled = true;
    private CanvasGroup _canvasGroup;
    private UiManager _uiManager;

    [SerializeField] private List<Animator> _animators;

    [SerializeField] private int _animationDelay;
    [SerializeField] private int _lifeTime;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;

        _uiManager = FindObjectOfType<UiManager>();
    }

    void Update()
    {
        if (_canvas.isActiveAndEnabled && _isEnabled)
        {
            _isEnabled = false;
            Invoke("DelayAnimation", _animationDelay);
            StartCoroutine("FadeIn");
        }
    }

    private void DelayAnimation()
    {
        foreach (var anim in _animators)
        {
            anim.Play("UI_Task_Animation");
        }
    }

    private IEnumerator FadeIn()
    {
        float timer = 2f;
        float t = 0.0f;

        while (t < timer)
        {
            t += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(0, 1, t / timer);
            yield return null;
        }

        if (_lifeTime > 0)
        {
            StartCoroutine("FadeOut");
        }
        
        yield return null;
    }
    
    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(_lifeTime+_animationDelay);
        
        float timer = 2f;
        float t = 0.0f;

        while (t < timer)
        {
            t += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1, 0, t / timer);
            yield return null;
        }

        _uiManager.LoadCanvas(_uiManager.Index+1);

        yield return null;
    }
}