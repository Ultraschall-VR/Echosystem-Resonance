using System;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class TriggerAvalange : MonoBehaviour
    {
        [SerializeField] private bool _triggered;
        [SerializeField] private GameObject[] _rocks;

        void Start()
        {
            _triggered = false;
            if (_triggered == true)
            {
                foreach (var rock in _rocks)
                {
                    gameObject.SetActive(true);
                    GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }
    }
}