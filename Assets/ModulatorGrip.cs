using UnityEngine;

public class ModulatorGrip : MonoBehaviour
{
    public void ChangeWaypoint()
    {
        transform.parent.GetComponent<Modulator>().ChangeWaypoint();
    }
}
