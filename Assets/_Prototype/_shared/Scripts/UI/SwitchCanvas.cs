using UnityEngine;

public class SwitchCanvas : MonoBehaviour
{
    private Canvas _canvas;

    [SerializeField] private Canvas _switchCanvas;
    void OnEnable()
    {
        _canvas = GetComponentInParent<Canvas>();
        _canvas.enabled = false;
        _switchCanvas.enabled = true;
    }
}
