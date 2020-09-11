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
            
                if (hit.GetComponent<Rigidbody>() != null)
                    hit.GetComponent<Rigidbody>().AddExplosionForce(_power*100, transform.position,
                        _power * 100, 3.0F);
            }

            _explode = false;
        }
    }
}