using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class LoadNextObjective : MonoBehaviour
{
    private void Start()
    {
        Observer.HudObjectives.NextObjective();
    }
}
