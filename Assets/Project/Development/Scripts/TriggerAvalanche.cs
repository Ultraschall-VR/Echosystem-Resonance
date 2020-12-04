using System;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class TriggerAvalanche : MonoBehaviour
    {
        [SerializeField] private bool _triggered;
        [SerializeField] private GameObject[] _rocks;

        private void Start()
        {
            _triggered = false;
            
        }

        private void Update()
        {
            if (_triggered == true)
            {
                SetRigidbodies();
            }
        }

        private void SetRigidbodies()
        {
            foreach (var rock in _rocks)
            {
                rock.SetActive(true);
                rock.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Whale"))
            {
                SetRigidbodies();
            }
            
        }
    }
}