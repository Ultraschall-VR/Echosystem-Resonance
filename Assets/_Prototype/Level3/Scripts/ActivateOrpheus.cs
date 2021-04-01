using UnityEngine;
using UnityEngine.VFX;

namespace Echosystem.Resonance.Prototyping
{
    public class ActivateOrpheus : MonoBehaviour
    {
        private BeaconSocket _beaconSocket;
        private bool _activated;
        private bool _deactivated;

        [SerializeField] private VisualEffect _orpheus;

        void Start()
        {
            _beaconSocket = GetComponent<BeaconSocket>();
            _orpheus.enabled = false;
        }

        void Update()
        {
            if (_beaconSocket.IsOccupied && !_activated)
            {
                _activated = true;
                _orpheus.enabled = true;
            }

            if (CollectibleManager.Index == 1 && !_deactivated)
            {
                _deactivated = true;
                _orpheus.gameObject.SetActive(false);
            }
        }
    }
}