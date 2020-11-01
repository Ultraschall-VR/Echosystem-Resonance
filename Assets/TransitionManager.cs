using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        FadeIn(Color.black);
    }

    public void FadeIn(Color color)
    {
        _panel.material.color = color;
        _animator.SetBool("FadeIn", true);
        _animator.SetBool("FadeOut", false);
    }

    public void FadeOut(Color color)
    {
        _panel.material.color = color;
        _animator.SetBool("FadeIn", false);
        _animator.SetBool("FadeOut", true);
    }
}
