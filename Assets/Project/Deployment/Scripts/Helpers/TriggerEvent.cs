using UnityEngine;

namespace Echosystem.Resonance.Helper
{
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _action;
        [SerializeField] AudioSource[] _audioAction;
        [SerializeField] private bool _triggered;
        private bool audioStarted = false;

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
                foreach (AudioSource i in _audioAction)
                {
                    i.Play();
                }

                // Sets audioStarted = true, so Player can't trigger it again
                audioStarted = true;
            }
        }
    }
}