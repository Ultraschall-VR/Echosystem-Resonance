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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
