using System;
using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Helper
{
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _actions;
        [SerializeField] private string _tag;

        private void Start()
        {
            HandleAction(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_tag == null)
            {
                if (other.CompareTag("Player"))
                {
                    HandleAction(true);
                }
            }
            else
            {
                if (other.CompareTag(_tag))
                {
                    HandleAction(true);
                }
            }
        }

        private void HandleAction(bool enabled)
        {
            foreach (var action in _actions)
            {
                action.enabled = enabled;
            }
        }
    }
}