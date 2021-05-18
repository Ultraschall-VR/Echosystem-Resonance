using System;
using UnityEngine;
using UnityEngine.VFX;

public class BeamGenerator : MonoBehaviour
{
    [SerializeField] private VisualEffect _muffle;
    [SerializeField] private VisualEffect _impact;
    [SerializeField] private LayerMask _layerMask;

    private LineRenderer _lineRenderer;

    private Modulator _modulator = null;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShootBeam(Transform raycastOrigin, Vector3 direction, Transform beamOrigin, float distance,
        ModulationType modulationType)
    {
        if (_modulator != null)
        {
            _modulator.Active = false;
        }
        
        RaycastHit hit;
        float offset = Time.time * 2;
        
        SetVisuals(direction, beamOrigin, modulationType, offset);
        
        if (Physics.Raycast(raycastOrigin.position, direction, out hit, distance, _layerMask))
        {
            _lineRenderer.SetPosition(1, hit.point);
            
            _impact.Play();
            _impact.gameObject.transform.position = _lineRenderer.GetPosition(1);
            
            if (hit.transform.GetComponent<Energizable>())
            {
                Energizable energizable = hit.transform.GetComponent<Energizable>();

                if (energizable.ModulationType == modulationType)
                {
                    energizable.Energize();
                }
            }

            if (hit.transform.GetComponent<Modulator>())
            {
                _modulator = hit.transform.GetComponent<Modulator>();
                _modulator.Active = true;
                _modulator.FormerForward = direction;
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, beamOrigin.position + direction * 200);
            _impact.Play();
            _impact.gameObject.transform.position = _lineRenderer.GetPosition(1);
        }
    }

    private void SetVisuals(Vector3 direction, Transform beamOrigin, ModulationType modulationType, float offset)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, beamOrigin.position);
        _lineRenderer.material = modulationType.BeamMaterial;
        _lineRenderer.material.SetTextureOffset("_BaseMap", new Vector2(-offset, 0));

        _muffle.SetVector4("Color", modulationType.BeamMaterial.color);
        _muffle.Play();
        _muffle.gameObject.transform.position = beamOrigin.position;
        _muffle.gameObject.transform.forward = direction;

        _impact.SetVector4("Color", modulationType.BeamMaterial.color);
    }

    public void DisableRenderer()
    {
        _lineRenderer.enabled = false;
        _muffle.Stop();
        _impact.Stop();

        if (_modulator != null)
            _modulator.Active = false;

        _modulator = null;
    }
}