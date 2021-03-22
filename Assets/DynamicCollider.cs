using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class DynamicCollider : MonoBehaviour
{
    [SerializeField] private bool _dead;
    private Collider _collider;
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_collider.bounds.Intersects(Observer.CurrentSilenceSphere.SphereCollider.bounds))
        {
            if (_dead)
            {
                GetComponent<Rigidbody>().detectCollisions = false;
            }
            else
            {
                GetComponent<Rigidbody>().detectCollisions = true;
            }
        }

        if (!_collider.bounds.Intersects(Observer.CurrentSilenceSphere.SphereCollider.bounds))
        {
            if (_dead)
            {
                GetComponent<Rigidbody>().detectCollisions = true;
            }
            else
            {
                GetComponent<Rigidbody>().detectCollisions = false;
            }
        }
    }
}