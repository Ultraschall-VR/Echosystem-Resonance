using TMPro;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class DebugUi_CurrentSilenceSphere : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        void Update()
        {
            if (Observer.CurrentSilenceSphere != null)
            {
                _text.text = Observer.CurrentSilenceSphere.name;
            }
            else
            {
                _text.text = null;
            }
        }
    }
}


