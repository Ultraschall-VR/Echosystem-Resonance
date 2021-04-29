using System.Collections.Generic;
using AmazingAssets.DynamicRadialMasks;
using UnityEngine;

public class NonVRPlayer : MonoBehaviour
{
    public Camera PlayerHead;
    public DRMGameObject DrmGameObject;
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
