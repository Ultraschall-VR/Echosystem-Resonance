using System;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class DestroyableObject : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();

        }

        private void Update()
        {
            _speed = _rigidbody.velocity.magnitude;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > 2)
            {
                _meshRenderer.enabled = false;
            }
        }
    }
}

