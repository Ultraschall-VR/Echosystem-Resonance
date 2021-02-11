using UnityEngine;

namespace Echosystem.Resonance.Helper
{
    public class TriggerBox : MonoBehaviour
    {
        public bool PlayerInTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerInTrigger = true;
            }
        }
    }
}