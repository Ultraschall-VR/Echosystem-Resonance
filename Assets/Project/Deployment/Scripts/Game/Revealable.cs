using System;
using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class Revealable : MonoBehaviour
    {
        [SerializeField] private Material _coveredMaterial;

        public bool Dynamic;
        private bool _routineRunning;
        private bool _break;

        private float _power;

        private void Awake()
        {
            GetComponent<MeshRenderer>().material = _coveredMaterial;
        }

        private void Update()
        {
            GetComponent<MeshRenderer>().material.SetFloat("Radius", _power);
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
    }
}