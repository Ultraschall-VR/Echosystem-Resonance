using System;
using UnityEngine;
using UnityEngine.VFX;

public class BeamGenerator : MonoBehaviour
{
    [SerializeField] private VisualEffect _muffle;
    [SerializeField] private VisualEffect _impact;

    [SerializeField] private Material _sineMat;
    [SerializeField] private Material _squareMat;
    [SerializeField] private Material _triangleMat;
    [SerializeField] private Material _pitchDownMat;
    [SerializeField] private Material _chorusMat;

    [SerializeField] private LayerMask _layerMask;
    
    private LineRenderer _lineRenderer;

    private Modulator _modulator = null;
    
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    public void ShootBeam(Transform raycastOrigin, Vector3 direction, Transform origin, WaveFormType waveFormType, float distance)
    {
        switch (waveFormType)
        {
            case WaveFormType.Sine:
                _lineRenderer.material = _sineMat;
                _muffle.SetVector4("Color", _sineMat.color);
                _impact.SetVector4("Color", _sineMat.color);
                
                break;
            
            case WaveFormType.Square:
                _lineRenderer.material = _squareMat;
                _muffle.SetVector4("Color", _squareMat.color);
                _impact.SetVector4("Color", _squareMat.color);
                
                break;
            
            case WaveFormType.Triangle:
                _lineRenderer.material = _triangleMat;
                _muffle.SetVector4("Color", _triangleMat.color);
                _impact.SetVector4("Color", _triangleMat.color);
                
                break;
            
            case WaveFormType.PitchDown:
                _lineRenderer.material = _pitchDownMat;
                _muffle.SetVector4("Color", _pitchDownMat.color);
                _impact.SetVector4("Color", _pitchDownMat.color);
                break;
            
            case WaveFormType.Chorus:
                _lineRenderer.material = _chorusMat;
                _muffle.SetVector4("Color", _chorusMat.color);
                _impact.SetVector4("Color", _chorusMat.color);
                break;
                
        }
        
        RaycastHit hit;
        float offset = Time.time * 2;
        
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, origin.position);
        _muffle.Play();
        _muffle.gameObject.transform.position = origin.position;
        _muffle.gameObject.transform.forward = direction;
        _lineRenderer.material.SetTextureOffset("_BaseMap", new Vector2(-offset, 0));
        
        if (Physics.Raycast(raycastOrigin.position, direction, out hit, distance, _layerMask))
        {
            _impact.Play();
            _impact.gameObject.transform.position = _lineRenderer.GetPosition(1);

            _lineRenderer.SetPosition(1, hit.point);

            if (hit.transform.GetComponent<Energizable>())
            {
                Energizable energizable = hit.transform.GetComponent<Energizable>();

                if (energizable.WaveForm == waveFormType)
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
            _lineRenderer.SetPosition(1, origin.position + direction*200);
            _impact.Play();
            _impact.gameObject.transform.position = _lineRenderer.GetPosition(1);
        }
    }
    
    public void DisableRenderer()
    {
        _lineRenderer.enabled = false;
        _muffle.Stop();
        _impact.Stop();
        
        if(_modulator != null)
            _modulator.Active = false;

        _modulator = null;
    }

    [Serializable]
    public enum WaveFormType
    {
        Sine,
        Square,
        Triangle,
        PitchDown,
        Chorus
    }
}
