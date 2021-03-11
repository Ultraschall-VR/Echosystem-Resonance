using Echosystem.Resonance.Prototyping;
using Echosystem.Resonance.UI;
using UnityEngine;

public class UI_Level_01 : MonoBehaviour
{
    private UiManager _uiManager;
    
    private void Start()
    {
        _uiManager = FindObjectOfType<UiManager>();
    }

    private void Update()
    {
        if (Observer.SilenceSphereExited)
        {
            _uiManager.LoadCanvas(1);
        }

        //if (Observer.CollectedObjects == Observer.MaxCollectibleObjects)
        //{
        //    _uiManager.LoadCanvas(2);
        //}
    }
}
