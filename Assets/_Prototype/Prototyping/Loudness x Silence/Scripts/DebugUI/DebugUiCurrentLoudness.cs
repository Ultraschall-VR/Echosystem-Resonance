using TMPro;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class DebugUiCurrentLoudness : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        void Update()
        {
            _text.text = "Loudness: " + Observer.LoudnessValue.ToString("F2");
        }
    }
}


