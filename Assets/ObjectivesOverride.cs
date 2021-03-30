using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class ObjectivesOverride : MonoBehaviour
{
    public List<HudObjective> HudObjectivesList;

    private void Update()
    {
        if (Observer.HudObjectives != null)
        {
            Observer.HudObjectives.HudObjectivesList = HudObjectivesList;
        }
    }
}
