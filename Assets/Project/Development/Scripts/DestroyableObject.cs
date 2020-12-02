using System;
using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class DestroyableObject : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private Collider _collider;
        [SerializeField] private Collider _capCollider;

        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private bool _particlesStopped;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _capCollider.enabled = false;
        }

        private void Update()
        {
            _speed = _rigidbody.velocity.magnitude;
            _particlesStopped = false;
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > 2)
            {
                _meshRenderer.enabled = false;
                _collider.enabled = false;
                _capCollider.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_meshRenderer.enabled == false && !_particlesStopped)
            {
                StartCoroutine(PlayParticles());
            }
        }

        private IEnumerator PlayParticles()
        {
            _particle.Play();
            yield return new WaitForSeconds(2f);
            
            _particle.Stop();
            yield return _particlesStopped = true;
        }
    }
}