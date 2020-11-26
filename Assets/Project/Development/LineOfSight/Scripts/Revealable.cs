using System;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class Revealable : MonoBehaviour
    {
        [SerializeField] private Material _coveredMaterial;

        public bool Dynamic;

        private void Awake()
        {
            GetComponent<MeshRenderer>().material = _coveredMaterial;
        }

        public void Reveal()
        {
            GetComponent<MeshRenderer>().material.SetFloat("Radius", 1);
        }
    }
}