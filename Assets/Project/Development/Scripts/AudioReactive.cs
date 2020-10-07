using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactive : MonoBehaviour
{
    [SerializeField] public List<MeshRenderer> Meshes;
    [SerializeField] private Material _audioReactiveMat;
    [SerializeField] private Material _uncoveredMat;
    
    private static readonly int ObjectPos = Shader.PropertyToID("ObjectPos");
    private static readonly int Radius = Shader.PropertyToID("Radius");

    [SerializeField] private bool _singleMesh;

    private bool _initialized = false;

    private float _power;

    private bool _conceal = false;
    
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_singleMesh)
        {
            Meshes.Add(GetComponent<MeshRenderer>());
        }
        
        foreach (var mesh in Meshes)
        {
            mesh.material = _audioReactiveMat;
        }
    }

    private void Update()
    {
        foreach (var mesh in Meshes)
        {
            mesh.material.SetFloat(Radius, _power * 500);
        }
    }

    public void Reveal(Vector3 position, float power)
    {
        foreach (var mesh in Meshes)
        {
            mesh.material.SetVector(ObjectPos, position);
            _power = power;
        }

        _conceal = false;
    }

    public void Conceal(float speed)
    {
        if (!_conceal)
        {
            StartCoroutine(Conceal(0,speed)); 
        }
    }

    public void Uncover()
    {
        foreach (var mesh in Meshes)
        {
            mesh.material = _uncoveredMat;
        }
    }
    
    private IEnumerator Conceal(float targetValue, float speed)
    {
        float t = 0;
        float timer = 0.5f;

        while (t <= timer)
        {
            _conceal = true;
            
            t += Time.fixedDeltaTime / speed;

            _power = Mathf.Lerp(_power, targetValue, t);
            
            yield return null;
        }
    }
}
