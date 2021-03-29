using Echosystem.Resonance.Prototyping;
using Echosystem.Resonance.UI;
using UnityEngine;

public class UI_Level_02 : MonoBehaviour
{
    private UiManager _uiManager;

    private bool _midGoal = false;
    private bool _allCollected = false;

    private void Start()
    {
        _uiManager = GetComponent<UiManager>();
    }

    private void Update()
    {
        if (CollectibleManager.MidGoal && !_midGoal)
        {
            _uiManager.LoadCanvas(_uiManager.Index+1);
            _midGoal = true;
        }
        
        if (CollectibleManager.AllCollected && !_allCollected)
        {
            _uiManager.LoadCanvas(_uiManager.Index+1);
            _allCollected = true;
        }
    }
}
