using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    
    [SerializeField] private KeyCode _toggleKey;

    void Update()
    {
        if (Input.GetKeyUp(_toggleKey))
        {
            _canvas.enabled = !_canvas.enabled;
        }
    }
}
