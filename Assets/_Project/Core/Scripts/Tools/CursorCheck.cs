using UnityEngine;

namespace Echosystem.Resonance.Tools
{
    public class CursorCheck : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}