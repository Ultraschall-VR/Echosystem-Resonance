using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class ObjectivesOverride : MonoBehaviour
{
    public List<HudObjective> HudObjectivesList;

    private void Start()
    {
        Invoke("Delay", 0.2f);
    }

    private void Delay()
    {
        if (Observer.HudObjectives != null)
        {
            Observer.HudObjectives.HudObjectivesList = HudObjectivesList;
        }
    }
}
