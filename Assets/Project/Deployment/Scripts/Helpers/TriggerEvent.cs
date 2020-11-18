using UnityEngine;

namespace Echosystem.Resonance.Helper
{
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _action;

        private void Start()
        {
            _action.enabled = false;
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