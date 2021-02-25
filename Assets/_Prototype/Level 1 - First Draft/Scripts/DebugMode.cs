using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour
{
    [SerializeField] private List<Behaviour> _componentsToDisable;

    private void Start()
    {
        if (!SceneSettings.Instance.DebugMode)
        {
            foreach (var comp in _componentsToDisable)
            {
                comp.enabled = false;
            }
        }
    }
}
