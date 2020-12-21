using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Echosystem.Resonance.Game
{
    public class DestroyableObject : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private Collider _collider;
        [SerializeField] private Collider _capCollider;

        [SerializeField] private VisualEffect _particle;
        [SerializeField] private bool _particlesStopped;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _capCollider.enabled = false;
            _particlesStopped = false;

            _particle.Stop();
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
                _collider.enabled = false;
                _capCollider.enabled = true;
            }

            if (!_meshRenderer.enabled && !_particlesStopped)
            {
                StartCoroutine(PlayParticles());
            }
        }

        private IEnumerator PlayParticles()
        {
            _particle.Play();
            yield return new WaitForSeconds(0.2f);

            _particle.Stop();
            yield return _particlesStopped = true;
            
            yield return new WaitForSeconds(1f);
            Destroy(this);
        }
    }
}