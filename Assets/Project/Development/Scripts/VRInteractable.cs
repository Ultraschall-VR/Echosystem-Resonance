using System;
using System.Collections.Generic;
using UnityEngine;

public class VRInteractable : MonoBehaviour
{
    [SerializeField] private Material _coveredMaterial;
    [SerializeField] private Material _uncoveredMaterial;
    [SerializeField] private MeshRenderer _mesh;

    public bool IsCovered;
    
    public Material ActiveMaterial;
    
    

    void Start()
    {
        // Deactivate collision with ramps which are for player movement only
        var ramps = GameObject.FindObjectOfType<Ramps>().RampColliders;

        foreach (var ramp in ramps)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), ramp);
        }
    }

    private void Update()
    {
        if (IsCovered)
        {
            ActiveMaterial = _coveredMaterial;
        }
        else
        {
            ActiveMaterial = _uncoveredMaterial;
        }
        
        _mesh.material = ActiveMaterial;
    }

    public void Uncover()
    {
        IsCovered = false;
    }

    public void Cover()
    {
        IsCovered = false;
    }
}
