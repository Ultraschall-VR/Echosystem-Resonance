using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class UICollectibles : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> _canvasGroups;

    private void Update()
    {
        if (CollectibleManager.Index > 0)
        {
            _canvasGroups[CollectibleManager.Index-1].alpha = 1f;
        }
    }
}
