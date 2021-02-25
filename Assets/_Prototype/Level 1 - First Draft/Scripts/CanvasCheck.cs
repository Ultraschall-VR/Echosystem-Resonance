using System.Collections.Generic;
using UnityEngine;

public class CanvasCheck : MonoBehaviour
{
    private Canvas _canvas;
    private bool _isEnabled = true;

    [SerializeField] private List<Animator> _animators;

    [SerializeField] private int _animationDelay;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (_canvas.isActiveAndEnabled && _isEnabled)
        {
            _isEnabled = false;
            Invoke("DelayAnimation", _animationDelay);
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
}
