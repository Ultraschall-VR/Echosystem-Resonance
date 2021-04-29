using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    public HudObjectives HudObjectives;
    
    [SerializeField] private List<GameObject> _hideComponents;

    public void HidePlayer()
    {
        foreach (var go in _hideComponents)
        {
            go.SetActive(false);
        }
    }
    
    public void ShowPlayer()
    {
        foreach (var go in _hideComponents)
        {
            go.SetActive(true);
        }
    }
}
