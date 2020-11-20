using System;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class AudioReactiveEnvironment : MonoBehaviour
    {
        private MeshRenderer _mesh;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _mesh = GetComponent<MeshRenderer>();
        }
    }
}


