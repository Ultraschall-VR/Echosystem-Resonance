using UnityEngine;

public class LineRendererCaster : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Material _validTeleportMaterial;
    [SerializeField] private Material _invalidTeleportMaterial;

    [SerializeField] private Material _grabMaterial;
    
    public Transform RaycastTarget;

    private Vector3 _arc;
    private Vector3 _center;
    
    private void Start()
    {
        Hide();
    }
    
    public void ShowValidTeleport(Vector3 origin, Vector3 target)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.material = _validTeleportMaterial;
        RaycastTarget.gameObject.SetActive(false);
        
        _center = (origin + target) * 0.5f;
        _center.y -= Vector3.Distance(origin, target);
        
        var relCenter = origin - _center;
        var relAimCenter = target - _center;

        float x = -0.0417f;
        var index = -1;

        while (x < 1.0f && index < _lineRenderer.positionCount -1) 
        {
            x += 0.0417f;
            index++;

            _arc = Vector3.Slerp(relCenter, relAimCenter, x);
            _lineRenderer.SetPosition(index, _arc + _center);
        }
    }

    public void ShowInvalidTeleport(Vector3 origin, Vector3 target)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.material = _invalidTeleportMaterial;
        RaycastTarget.gameObject.SetActive(false);
        
        _center = (origin + target) * 0.5f;
        _center.y -= Vector3.Distance(origin, target);
        
        var relCenter = origin - _center;
        var relAimCenter = target - _center;

        float x = -0.0417f;
        var index = -1;

        while (x < 1.0f && index < _lineRenderer.positionCount -1) 
        {
            x += 0.0417f;
            index++;

            _arc = Vector3.Slerp(relCenter, relAimCenter, x);
            _lineRenderer.SetPosition(index, _arc + _center);
        }
    }
    
    public void ShowGrab(Vector3 origin, Vector3 target)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.widthMultiplier = 0.5f;
        _lineRenderer.material = _grabMaterial;
        RaycastTarget.gameObject.SetActive(false);
        
        _center = (origin + target) * 0.5f;
        _center.y -= Vector3.Distance(origin, target) *4;
        
        var relCenter = origin - _center;
        var relAimCenter = target - _center;

        float x = -0.0417f;
        var index = -1;

        while (x < 1.0f && index < _lineRenderer.positionCount -1) 
        {
            x += 0.0417f;
            index++;

            _arc = Vector3.Slerp(relCenter, relAimCenter, x);
            _lineRenderer.SetPosition(index, _arc + _center);
        }
    }

    public void Hide()
    {
        _lineRenderer.enabled = false;
    }
}
