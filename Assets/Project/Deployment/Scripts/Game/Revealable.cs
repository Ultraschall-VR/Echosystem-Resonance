using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class Revealable : MonoBehaviour
    {
        [SerializeField] private Material _coveredMaterial;
        
        private Material _initMaterial;
        
        private Collider _collider;
        
        private MeshRenderer _meshRenderer;

        private Animator _animator;

        public bool Dynamic;
        private bool _uncovered;
        private bool _routineRunning;
        private bool _break;

        private float _power;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (GetComponent<Animator>() != null)
            {
                _animator = GetComponent<Animator>();
            }
            else
            {
                _animator = null;
            }
            
            _collider = GetComponent<Collider>();
            _initMaterial = GetComponent<MeshRenderer>().material;
            _meshRenderer = GetComponent<MeshRenderer>();

            _meshRenderer.material = _coveredMaterial;
            _collider.enabled = false;
        }

        private void Update()
        {
            _meshRenderer.material.SetFloat("Radius", _power);
        }

        public void Reveal()
        {
            if (!_routineRunning)
                StartCoroutine(RevealRoutine());
        }

        private IEnumerator RevealRoutine()
        {
            _routineRunning = true;
            float timer = 1f;
            float t = 0.0f;

            while (t <= timer)
            {
                t += Time.deltaTime;
                _power = Mathf.Lerp(0, 1, t / timer);

                yield return null;
            }

            timer = 10f;
            t = 0.0f;

            while (t <= timer)
            {
                t += Time.deltaTime;
                _power = Mathf.Lerp(1, 0, t / timer);

                yield return null;
            }

            _routineRunning = false;

            yield return null;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(_power < 0.1f)
                return;
            
            if (other.gameObject.GetComponent<BlasterBullet>())
            {
                Uncover();
            }
        }

        private void Uncover()
        {
            _uncovered = true;
            _collider.enabled = true;
            _meshRenderer.material = _initMaterial;

            if (_animator != null)
                GetComponent<Animator>().enabled = false;
            
        }
    }
}