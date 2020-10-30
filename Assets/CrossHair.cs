﻿using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _hitColor;

    private MeshRenderer _meshRenderer;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * 10, Color.black);
        
        if (Physics.Raycast(transform.position, transform.forward * 10, out hit, Mathf.Infinity))
        {
            if (!hit.transform.gameObject.isStatic)
            {
                _meshRenderer.material.SetColor(EmissionColor, _hitColor);
                _meshRenderer.material.color = _hitColor;
            }
            else
            {
                _meshRenderer.material.SetColor(EmissionColor, _defaultColor);
                _meshRenderer.material.color = _defaultColor;
            }
        }
        else
        {
            _meshRenderer.material.SetColor(EmissionColor, _defaultColor);
            _meshRenderer.material.color = _defaultColor;
        }
    }
}
