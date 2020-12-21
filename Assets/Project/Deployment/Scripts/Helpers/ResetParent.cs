using UnityEngine;

namespace Echosystem.Resonance.Helper
{
    public class ResetParent : MonoBehaviour
    {
        [SerializeField] private Transform _syncTransform;

        void Start()
        {
            transform.parent = null;
        }

        void Update()
        {
            if (_syncTransform != null)
            {
                transform.position = _syncTransform.position;
            }
        }
    }
}