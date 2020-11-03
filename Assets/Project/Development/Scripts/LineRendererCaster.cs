using UnityEngine;

public class LineRendererCaster : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Material _validTeleportMaterial;
    [SerializeField] private Material _invalidTeleportMaterial;

    [SerializeField] private MeshRenderer _teleportTargetRenderer;
    [SerializeField] private Material _validTeleportTargetMaterial;
    [SerializeField] private Material _invalidTeleportTargetMaterial;
    
    [SerializeField] private Material _grabMaterial;
    [SerializeField] private Material _uiMaterial;
    
    public Transform RaycastTarget;

    private Vector3 _arc;
    private Vector3 _center;
    
    private void Start()
    {
        Hide();
    }

    private void DrawLineRenderer(Vector3 origin, Vector3 target, float arcMultiplier, Material mat, bool showRaycastTarget)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.material = mat;
        
        RaycastTarget.gameObject.SetActive(showRaycastTarget);

        RaycastHit hit;

        if (Physics.Raycast(RaycastTarget.transform.position, -RaycastTarget.transform.up, out hit, Mathf.Infinity))
        {
            RaycastTarget.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }

        _center = (origin + target) * 0.5f;
        _center.y -= Vector3.Distance(origin, target) * arcMultiplier;
        
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
    
    
    public void ShowValidTeleport(Vector3 origin, Vector3 target, float arcMultiplier)
    {
        DrawLineRenderer(origin, target, arcMultiplier, _validTeleportMaterial, true);
        _teleportTargetRenderer.material = _validTeleportTargetMaterial;
    }

    public void ShowInvalidTeleport(Vector3 origin, Vector3 target, float arcMultiplier)
    {
        DrawLineRenderer(origin, target, arcMultiplier, _invalidTeleportMaterial, true);
        _teleportTargetRenderer.material = _invalidTeleportTargetMaterial;
    }
    
    public void ShowGrab(Vector3 origin, Vector3 target, float arcMultiplier)
    {
        DrawLineRenderer(origin, target, arcMultiplier, _grabMaterial, false);
    }

    public void ShowUiRaycast(Vector3 origin, Vector3 target, float arcMultiplier)
    {
        DrawLineRenderer(origin, target, arcMultiplier, _uiMaterial, false);
    }

    public void Hide()
    {
        _lineRenderer.enabled = false;
        RaycastTarget.gameObject.SetActive(false);
    }
}
