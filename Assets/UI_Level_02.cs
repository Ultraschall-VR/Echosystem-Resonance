using Echosystem.Resonance.Prototyping;
using Echosystem.Resonance.UI;
using UnityEngine;

public class UI_Level_02 : MonoBehaviour
{
    private UiManager _uiManager;

    private void Start()
    {
        _uiManager = GetComponent<UiManager>();
    }

    private void Update()
    {
        if (Observer.CollectedObjects == Observer.MaxCollectibleObjects)
        {
            _uiManager.FadeOut();
        }
    }
}
