using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class UI_Level_01 : MonoBehaviour
{
    private bool _objective1 = false;

    [SerializeField] private GameObject _worldmarkerEnd;
    
    private void Update()
    {
        if (CollectibleManager.AllCollected && !_objective1)
        {
            Observer.HudObjectives.NextObjective();
            _objective1 = true;
            _worldmarkerEnd.SetActive(true);
        }
    }
}
