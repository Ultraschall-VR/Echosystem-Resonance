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