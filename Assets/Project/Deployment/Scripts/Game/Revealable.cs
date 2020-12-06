using System.Collections;
using Echosystem.Resonance.Game;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class Revealable : MonoBehaviour
    {
        [SerializeField] private Material _coveredMaterial;
        private Material _initMaterial;
        private Collider _collider;

        private bool _uncovered;
            
        public bool Dynamic;
        private bool _routineRunning;
        private bool _break;

        private float _power;

        private void Awake()
        {
            _initMaterial = GetComponent<MeshRenderer>().material;
            GetComponent<MeshRenderer>().material = _coveredMaterial;
        }

        private void Update()
        {
            GetComponent<MeshRenderer>().material.SetFloat("Radius", _power);

            if (_power > 0.1f || _uncovered)
            {
                _collider.enabled = true;
            }
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
                _uncovered = true;
                GetComponent<MeshRenderer>().material = _initMaterial;
            }
        }
    }
}