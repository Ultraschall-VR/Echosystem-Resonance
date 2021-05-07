using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class ModulatorController : MonoBehaviour
{
    void Update()
    {
        if (Observer.FocusedGameObject != null)
        {
            if (Observer.FocusedGameObject.GetComponent<ModulatorGrip>())
            {
                ModulatorGrip modulatorGrip = Observer.FocusedGameObject.GetComponent<ModulatorGrip>();

                if (Vector3.Distance(modulatorGrip.transform.position, Observer.PlayerHead.transform.position) < 8f)
                {
                    NonVrInput(modulatorGrip);
                }
            }
        }
    }

    private static void NonVrInput(ModulatorGrip modulatorGrip)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            modulatorGrip.ChangeWaypoint();
        }
    }
}
