using UnityEngine;

namespace Echosystem.Resonance.AI
{
    public class AttackDroneRange : MonoBehaviour
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