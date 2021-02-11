using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class VRInteraction : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _actionClass;

        public float Power = 0.0f;
        private bool _isIncreasing = false;

        private float _activationThreshold = 10.0f;

        public void IncreasePower()
        {
            if (!_isIncreasing)
            {
                _isIncreasing = true;
                StartCoroutine(ManagePower());
            }
        }

        public void DecreasePower()
        {
            if (_isIncreasing)
            {
                _isIncreasing = false;
                StartCoroutine(ManagePower());
            }
        }

        private void Update()
        {
            if (Power >= _activationThreshold && !_actionClass.enabled)
            {
                _actionClass.enabled = true;
            }
        }

        IEnumerator ManagePower()
        {
            while (_isIncreasing && Power < _activationThreshold)
            {
                Power += Time.deltaTime;
                yield return null;
            }

            while (!_isIncreasing && Power > 0)
            {
                Power -= Time.deltaTime;
                yield return null;
            }
        }
    }
}