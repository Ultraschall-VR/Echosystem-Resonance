using System;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    public Transform RaycastTarget;

    private Vector3 _arc;
    private Vector3 _center;

    private void Start()
    {
        Hide();
    }
    
    public void Show(Transform target)
    {
        _lineRenderer.enabled = true;
        
        _center = (transform.position + target.position) * 0.5f;
        _center.y -= 30;

        var relCenter = transform.position - _center;
        var relAimCenter = target.position - _center;

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
