using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleTest : MonoBehaviour
{
    [SerializeField] private VisualEffect _particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _particleSystem.SetVector3("Position", transform.forward);
    }
}
