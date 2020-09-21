using System;
using UnityEngine;
using UnityEngine.UI;

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
    
    public void Show(Vector3 origin, Vector3 target)
    {
        _lineRenderer.enabled = true;
        RaycastTarget.gameObject.SetActive(true);
        
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

    public void Hide()
    {
        _lineRenderer.enabled = false;
        RaycastTarget.gameObject.SetActive(false);
    }
    
}
