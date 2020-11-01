using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private List<Collider> _colliders;
    [SerializeField] private CanvasGroup _canvasGroup;
    
    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
    }

    public void EnableColliders(bool enable)
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = enable;
        }
    }
}
