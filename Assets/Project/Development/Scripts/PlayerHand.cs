using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    
    private Vector3 _arc;
    private Vector3 _center;

    private void Start()
    {
        HideLineRenderer();
    }

    public void ShowLineRenderer(Vector3 origin, Vector3 target)
    {
        _lineRenderer.enabled = true;

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

    public void HideLineRenderer()
    {
        _lineRenderer.enabled = false;
    }
}
