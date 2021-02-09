using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class IgnorePlayerCollision : MonoBehaviour
    {
        [SerializeField] private bool _enabled;

        void Start()
        {
            if (_enabled)
            {
                Invoke("ProlongedStart", 2f);
            }
        }

        void ProlongedStart()
        {
            Physics.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent<Collider>(),
                GetComponent<Collider>());
        }
    }
}