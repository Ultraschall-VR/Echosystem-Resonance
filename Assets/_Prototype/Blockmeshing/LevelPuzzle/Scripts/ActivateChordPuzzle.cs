using System;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class ActivateChordPuzzle : MonoBehaviour
    {
        private void Start()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}