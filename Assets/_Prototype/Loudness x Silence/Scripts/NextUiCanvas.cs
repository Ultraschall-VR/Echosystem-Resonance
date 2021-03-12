using Echosystem.Resonance.UI;
using UnityEngine;

public class NextUiCanvas : MonoBehaviour
{
    private UiManager _uiManager;

    void Start()
    {
        _uiManager = FindObjectOfType<UiManager>();
        _uiManager.LoadCanvas(_uiManager.Index+1);
    }
}
