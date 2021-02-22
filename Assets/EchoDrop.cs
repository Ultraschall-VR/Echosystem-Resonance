using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    [RequireComponent(typeof(SilenceSphere))]
    public class EchoDrop : MonoBehaviour
    {
        private SilenceSphere _silenceSphere;
        private Rigidbody _rb;

        private void Start()
        {
            _silenceSphere = GetComponent<SilenceSphere>();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.collider.CompareTag("Player"))
                return;

            _silenceSphere.DecreaseSize();
            _rb.useGravity = false;
            _rb.isKinematic = true;
        }
    }
}


