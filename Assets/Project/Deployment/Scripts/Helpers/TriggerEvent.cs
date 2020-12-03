using UnityEngine;

namespace Echosystem.Resonance.Helper
{
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _action;
        [SerializeField] private bool _triggered;

        private void Awake()
        {
            _action.enabled = false;
        }

        private void Update()
        {
            if (_triggered)
            {
                _action.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _action.enabled = true;
            }
        }
    }
}