using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class SetAreaIndex : MonoBehaviour
    {
        [SerializeField] private int _index;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Observer.Player)
            {
                Observer.AreaIndex = _index;
            }
        }
    }
}