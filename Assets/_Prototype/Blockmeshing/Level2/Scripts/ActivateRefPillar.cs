using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class ActivateRefPillar : MonoBehaviour
    {
        private BeaconSocket _beaconSocket;
        private bool _activated;

        [SerializeField] private GameObject _refPillar;

        void Start()
        {
            _beaconSocket = GetComponent<BeaconSocket>();
            _refPillar.gameObject.SetActive(false);
        }

        void Update()
        {
            if (_beaconSocket.IsOccupied && !_activated)
            {
                _activated = true;
                _refPillar.gameObject.SetActive(true);
            }
        }
    }
}