using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInteractable : MonoBehaviour
{
    public Vector3 GrabScale;
    public Material DefaultMaterial;
    [HideInInspector] public Vector3 Scale;
    
    // Start is called before the first frame update
    void Start()
    {
        Scale = transform.localScale;
        
        // Deactivate collision with ramps which are for player movement only
        var ramps = GameObject.FindObjectOfType<Ramps>().RampColliders;

        foreach (var ramp in ramps)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), ramp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
