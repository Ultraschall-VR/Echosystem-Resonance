using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;
using UnityEngine.Rendering.Universal;

public class PostProcessingExplosion : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private Transform _geo;

    private Bloom _bloomLayer = null;
    private ColorAdjustments _colorAdjustments = null;

    private TransitionManager _transitionManager;

    private bool _exploded = false;
    private bool _postProSet = false;

    private void Start()
    {
        volume.sharedProfile.TryGet<Bloom>(out _bloomLayer);
        volume.sharedProfile.TryGet<ColorAdjustments>(out _colorAdjustments);

        Initialize();

        StartCoroutine(Explode());
    }

    private void Initialize()
    {
        _bloomLayer.intensity.value = 1f;
        _colorAdjustments.postExposure.value = 1f;
    }

    private void DeactivateBloom()
    {
        _bloomLayer.intensity.value = 0f;
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(10f);

        float t = 0.0f;
        float timer = 150f;

        while (t < timer)
        {
            t += Time.deltaTime * 20;

            _bloomLayer.intensity.value = t;
            _colorAdjustments.postExposure.value = t / 5;

            if (t >= timer)
            {
                _exploded = true;
            }
            
            yield return null;
        }
    }

    private void Update()
    {
        if (_exploded && !_postProSet)
        {
            FindObjectOfType<VisibilityController>().HidePlayer();
            FindObjectOfType<Credits>().Show();
            _geo.gameObject.SetActive(false);
            Invoke("Initialize", 2f);
            Invoke("DeactivateBloom", 3f);
            StopAllCoroutines();
            _postProSet = true;
        }
    }
}