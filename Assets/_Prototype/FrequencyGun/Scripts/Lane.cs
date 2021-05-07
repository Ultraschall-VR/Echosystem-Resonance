using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private List<Energizable> _energizables;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        
        _meshRenderer.enabled = false;
    }

    void Update()
    {
        if (!_meshRenderer.enabled)
        {
            foreach (var energizable in _energizables)
            {
                if (!energizable.Energized)
                {
                    return;
                }
            }
            _meshRenderer.enabled = true;
        }
    }
}
