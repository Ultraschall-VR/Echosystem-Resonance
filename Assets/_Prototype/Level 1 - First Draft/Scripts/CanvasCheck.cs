using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCheck : MonoBehaviour
{
    private Canvas _canvas;
    private bool _isEnabled = true;
    private CanvasGroup _canvasGroup;

        [SerializeField] private List<Animator> _animators;

    [SerializeField] private int _animationDelay;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    void Update()
    {
        if (_canvas.isActiveAndEnabled && _isEnabled)
        {
            _isEnabled = false;
            Invoke("DelayAnimation", _animationDelay);
            StartCoroutine("Fade");
        }
    }

    private void DelayAnimation()
    {
        foreach (var anim in _animators)
        {
            anim.Play("FrameText_UpperLeft");
            anim.Play("FrameBox_BottomRight");
        }
    }

    private IEnumerator Fade()
    {
        float timer = 2f;
        float t = 0.0f;

        while (t < timer)
        {
            t += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(0, 1, t / timer);
            yield return null;
        }
        yield return null;
    }
}
