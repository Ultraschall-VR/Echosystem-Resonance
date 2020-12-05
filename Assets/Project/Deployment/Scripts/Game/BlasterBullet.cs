using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.AI;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

namespace Echosystem.Resonance.Game
{
    public class BlasterBullet : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> _meshes;
        [SerializeField] private Light _light;
        [SerializeField] private VisualEffect _particles;
        [SerializeField] private AudioSource _impactAudioSource;
        [SerializeField] private AudioSource _loopAudioSource;

        private bool _objectHit;
        private Rigidbody _rb;
        private Vector3 _offset;

        private float _maxLifeTime = 8f;
        private float _lifeTime;
        private bool _maxLifeTimeReached;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _offset = transform.forward; // +
            new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f));

            float tempMax = _maxLifeTime;
            
            _maxLifeTime = Random.Range(5, tempMax);
        }

        private void Update()
        {
            foreach (var collider in FindObjectsOfType<OverdriveBullet>().ToList())
            {
                Physics.IgnoreCollision(collider.GetComponent<Collider>(), GetComponent<Collider>());
            }

            _lifeTime += Time.deltaTime;

            if (_lifeTime >= _maxLifeTime)
            {
                _maxLifeTimeReached = true;
            }

            if (_objectHit || _maxLifeTimeReached)
            {
                foreach (var mesh in _meshes)
                {
                    mesh.enabled = false;
                }

                if (!_impactAudioSource.isPlaying)
                    _impactAudioSource.PlayOneShot(_impactAudioSource.clip);
                
                
                _loopAudioSource.Stop();
                _particles.SetFloat("SpawnRate", 0f);
                _light.enabled = false;

                StartCoroutine(DestroyBullet());
            }
        }

        private void FixedUpdate()
        {
            if (_objectHit)
                return;

            _rb.velocity = (transform.forward + _offset) * 15;
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(2f);
            Destroy(this.gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<BlasterBullet>())
                return;

            if (other.gameObject.GetComponent<OverdriveBullet>())
                return;

            if (other.gameObject.CompareTag("Player"))
                return;

            if (other.gameObject.GetComponent<AttackDrone>())
            {
                other.gameObject.GetComponent<AttackDrone>().Life--;
            }

            _objectHit = true;
        }
    }
}