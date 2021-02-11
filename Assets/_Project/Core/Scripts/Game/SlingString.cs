using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class SlingString : MonoBehaviour
    {
        [SerializeField] private LineRenderer _sling;

        private void Update()
        {
            if (_sling.enabled)
            {
                _sling.SetPosition(0, transform.position);
            }
        }
    }
}