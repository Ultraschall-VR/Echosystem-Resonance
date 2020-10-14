using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTest : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _renderers;
    [SerializeField] private float _radius;

    [SerializeField] private float _radiusMultiplier;
    
    private static readonly int ObjectPos = Shader.PropertyToID("ObjectPos");
    private static readonly int Radius = Shader.PropertyToID("Radius");

    void Update()
    {
        foreach (var rend in _renderers)
        {
            rend.material.SetVector(ObjectPos, this.transform.position);
            rend.material.SetFloat(Radius, transform.localScale.x * _radiusMultiplier);
        }
    }

    private void Start()
    {
        Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(), this.GetComponent<Collider>());
    }
}
