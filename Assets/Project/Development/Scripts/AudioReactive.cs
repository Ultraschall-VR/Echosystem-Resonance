using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioReactive : MonoBehaviour
{
    [SerializeField] public List<MeshRenderer> Meshes;
    [SerializeField] public List<Collider> Colliders;
    [SerializeField] private Material _audioReactiveMat;
    private Rigidbody _rb;
    private Material _uncoveredMat;
    
    private static readonly int ObjectPos = Shader.PropertyToID("ObjectPos");
    private static readonly int Radius = Shader.PropertyToID("Radius");

    [SerializeField] private bool _singleMesh;

    private bool _initialized = false;

    private float _power;

    private bool _conceal = false;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Initialize();
    }

    private void Initialize()
    {
        if (_singleMesh)
        {
            Meshes.Add(GetComponent<MeshRenderer>());
            Colliders.Add(GetComponent<Collider>());
        }
        
        foreach (var mesh in Meshes)
        {
            _uncoveredMat = mesh.material;
            mesh.material = _audioReactiveMat;
        }

        Cover();
    }

    private void Update()
    {
        foreach (var mesh in Meshes)
        {
            mesh.material.SetFloat(Radius, _power);
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
            mesh.shadowCastingMode = ShadowCastingMode.On;
            gameObject.tag = "TeleportArea";
            gameObject.layer = 10;
        }
    }

    public void Cover()
    {
        foreach (var mesh in Meshes)
        {
            mesh.material = _audioReactiveMat;
            mesh.shadowCastingMode = ShadowCastingMode.Off;
            gameObject.tag = "Covered";
            gameObject.layer = 13;
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
