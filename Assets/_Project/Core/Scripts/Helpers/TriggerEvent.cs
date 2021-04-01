using UnityEngine;

namespace Echosystem.Resonance.Helper
{
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _action;
        [SerializeField] AudioSource[] _audioSources;
        public bool Triggered;
        private bool _audioStarted = false;

        private void Awake()
        {
            if(_action == null)
                return;
            
            _action.enabled = false;
        }

        private void Update()
        {
            if(_action == null)
                return;
            
            if (Triggered)
            {
                _action.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Triggered = true;
                
                foreach (AudioSource i in _audioSources)
                {
                    i.Play();
                }
                
                _audioStarted = true;
                
                if(_action == null)
                    return;
                
                _action.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Triggered = false;
            }
        }
    }
}