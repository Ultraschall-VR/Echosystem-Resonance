using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class UIToolTipp : MonoBehaviour
    {
        public string Text;

        public void Show()
        {
            transform.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            transform.gameObject.SetActive(false);
        }
    }
}


