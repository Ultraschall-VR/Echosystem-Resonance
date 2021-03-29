using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private List<DebugUIObject> _debugUiObjects;

        public enum DebugUIItem
        {
            GameTime,
            CurrentSilenceSphere,
            CurrentLoudness
        }
    }
}

