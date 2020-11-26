using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class RevealerDynamic : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Revealable>().Dynamic)
            {
                Revealable revealable = other.gameObject.GetComponent<Revealable>();
                revealable.Reveal();
            }
        }
    }
}

