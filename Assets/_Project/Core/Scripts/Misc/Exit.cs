using UnityEngine;

namespace Echosystem.Resonance
{
    public class Exit : MonoBehaviour
    {
        private void OnEnable()
        {
            Application.Quit();
        }
    }
}