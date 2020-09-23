using System;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private bool _explode;
    private float _power;
    
    public void ShockWave(float power)
    {
        _power = power;
        _explode = true;
    }

    private void FixedUpdate()
    {
        if (_explode)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _power);
            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.name == "Player")
                {
                    return;
                }

                if (hit.GetComponent<Rigidbody>())
                {
                    if (hit.GetComponent<VRInteractable>())
                    {
                        hit.GetComponent<VRInteractable>().Uncover();
                    }
                    
                    hit.GetComponent<Rigidbody>().AddExplosionForce(_power*5, transform.position,
                        _power, 2.0F);
                }
            }
            _explode = false;
        }
    }
}