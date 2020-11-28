using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class EchoBlasterBullet : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _meshes;
    [SerializeField] private Light _light;
    [SerializeField] private VisualEffect _particles;

    private bool _objectHit;
    private Rigidbody _rb;
    private Vector3 _offset;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _offset = transform.forward +
                  new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f));
    }

    private void Update()
    {
        if (_objectHit)
        {
            foreach (var mesh in _meshes)
            {
                mesh.enabled = false;
            }

            _particles.SetFloat("SpawnRate", 0f);
            _light.enabled = false; 
        }
    }

    private void FixedUpdate()
    {
        if(_objectHit)
            return;
        
        _rb.velocity = (transform.forward + _offset) *10;
    }

    private void OnCollisionEnter(Collision other)
    {
        _rb.useGravity = true;
        _objectHit = true;
    }
}
