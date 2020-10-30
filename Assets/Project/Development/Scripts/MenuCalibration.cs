using UnityEngine;

public class MenuCalibration : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private RectTransform _canvas;
    
    private void OnEnable()
    {
        transform.position = _camera.forward * 2;
        
        Vector3 pos = new Vector3(transform.position.x, _canvas.sizeDelta.y/2, transform.position.z);

        transform.position = pos;
    }
}
