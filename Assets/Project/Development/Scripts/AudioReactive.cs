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

    public float Power;
    public bool Uncovered = false;

    private bool _conceal = false;

    private Animator _anim;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        
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
            mesh.material.SetFloat(Radius, Power);
        }

        ControllColliders();
    }

    private void ControllColliders()
    {
        if (!Uncovered)
        {
            if (Power < 0.001f)
            {
                foreach (var collider in Colliders)
                {
                    collider.enabled = false;
                }
            }
            else
            {
                foreach (var collider in Colliders)
                {
                    collider.enabled = true;
                }
            }
        }
        else
        {
            foreach (var collider in Colliders)
            {
                collider.enabled = true;
            }
        }
    }

    public void Reveal(Vector3 position, float power)
    {
        foreach (var mesh in Meshes)
        {
            mesh.material.SetVector(ObjectPos, position);
            Power = power/Vector3.Distance(position, transform.position);
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
            Uncovered = true;
            
            if(_anim != null)

            _anim.enabled = false;
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
            
            if(_anim != null)
            
            _anim.enabled = true;
        }
    }
    
    private IEnumerator Conceal(float targetValue, float speed)
    {
        float t = 0.01f;
        float timer = Power;

        while (t <= timer)
        {
            _conceal = true;
            
            t += Time.deltaTime / speed;

            Power = Mathf.Lerp(Power, targetValue, t);
            
            yield return null;
        }
    }
}
