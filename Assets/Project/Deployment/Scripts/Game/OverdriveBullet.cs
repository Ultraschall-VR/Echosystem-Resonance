using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Echosystem.Resonance.Game
{
    public class OverdriveBullet : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> _meshes;
        [SerializeField] private Light _light;
        [SerializeField] private GameObject _blasterBulletPrefab;

        private bool _objectHit;
        private Rigidbody _rb;
        private Vector3 _offset;

        private bool _bulletsInstantiated;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _offset = transform.forward +
                      new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f),
                          Random.Range(-0.02f, 0.02f));
        }

        private void Update()
        {
            if (_objectHit)
            {
                foreach (var mesh in _meshes)
                {
                    mesh.enabled = false;
                }

                _light.enabled = false;

                InstantiateBullets();
                Destroy(this.gameObject);
            }
        }

        private void InstantiateBullets()
        {
            if (_bulletsInstantiated)
                return;

            for (int i = 0; i < 4; i++)
            {
                Instantiate(_blasterBulletPrefab, transform.position - transform.forward,
                    Quaternion.Euler(90 * i, 0, 0));

                for (int j = 0; j < 4; j++)
                {
                    Instantiate(_blasterBulletPrefab, transform.position - transform.forward,
                        Quaternion.Euler(90 * i, 90 * j, 0));

                    for (int k = 0; k < 4; k++)
                    {
                        Instantiate(_blasterBulletPrefab, transform.position - transform.forward,
                            Quaternion.Euler(90 * i, 90 * j, 90 * k));
                    }
                }
            }

            _bulletsInstantiated = true;
        }

        private void FixedUpdate()
        {
            if (_objectHit)
                return;

            _rb.velocity = (transform.forward + _offset) * 10;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<BlasterBullet>())
                return;

            if (other.gameObject.GetComponent<OverdriveBullet>())
                return;
            
            if (other.gameObject.CompareTag("Player"))
                return;
            
            _rb.useGravity = true;
            _objectHit = true;
        }
    }
}