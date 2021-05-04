using UnityEngine;
using UnityEngine.VFX;

public class FrequencyGun : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _tip;

    [SerializeField] private VisualEffect _muffle;
    [SerializeField] private VisualEffect _impact;

    private LineRenderer _lineRenderer;
    
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        DisableRenderer();
    }
    
    void Update()
    {
        if(SceneSettings.Instance.VREnabled)
            VrInput();
        else
            NonVrInput();
    }

    private void NonVrInput()
    {
        if (Input.GetMouseButton(0))
            ShootRay();
        else
            DisableRenderer();
        
        if(Input.GetMouseButtonDown(0))
            ShootBlast();
    }

    private void DisableRenderer()
    {
        _lineRenderer.enabled = false;
        _muffle.Stop();
        _impact.Stop();
    }

    private void ShootRay()
    {
        RaycastHit hit;
        float offset = Time.time;
        
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, _tip.position);
        _muffle.Play();
        _muffle.gameObject.transform.position = _tip.position;
        _muffle.gameObject.transform.forward = _tip.forward;
        _lineRenderer.material.SetTextureOffset("_BaseMap", new Vector2(-offset, 0));

        
        if (Physics.Raycast(_hand.position, _hand.transform.forward, out hit, Mathf.Infinity))
        {
            if(hit.transform.CompareTag("Player"))
                return;
            
            _impact.Play();
            _impact.gameObject.transform.position = hit.point;

            _lineRenderer.SetPosition(1, hit.point);
            
            if (hit.transform.GetComponent<Energizable>())
            {
                Energizable energizable = hit.transform.GetComponent<Energizable>();
            }
        }
        else
        {
            _impact.Stop();
            _lineRenderer.SetPosition(1, _tip.position + _tip.forward*100);
        }
        
        
        // Draw Ray
        // Draw LineRenderer 
        // Check for Collision if LineRenderer pos == Ray.hit.point
        // Draw Impulse
        // Charge Target
    }

    private void ShootBlast()
    {
        // Implement
    }

    private void VrInput()
    {
        // Implement
    }
}
