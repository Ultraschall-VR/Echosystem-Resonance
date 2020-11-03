using System;
using System.Collections;
using UnityEngine;

public class VRInteraction : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _actionClass;

    private float _power = 0.0f;
    private bool _isIncreasing = false;
    
    public void IncreasePower()
    {
        if (!_isIncreasing)
        {
            _isIncreasing = true;
            StartCoroutine(ManagePower());
        }
    }

    public void DecreasePower()
    {
        if (_isIncreasing)
        {
            _isIncreasing = false;
            StartCoroutine(ManagePower());
        }
    }

    private void Start()
    {
        IncreasePower();
    }

    private void Update()
    {
        Debug.Log(_power);
    }

    IEnumerator ManagePower()
    {
        float timer = 2.0f;
        float t = 0.0f;
        
        while (_isIncreasing && _power < 1)
        {
            t += Time.deltaTime;
            _power = Mathf.Lerp(_power, 1, t);
            
            yield return null;
        }

        while (!_isIncreasing && _power > 0)
        {
            t += Time.deltaTime;
            _power = Mathf.Lerp(_power, 0, t);
            
            yield return null;
        }
    }
}
