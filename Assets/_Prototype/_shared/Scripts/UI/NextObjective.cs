using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class NextObjective : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Observer.HudObjectives.NextObjective();
        }
    }
}
