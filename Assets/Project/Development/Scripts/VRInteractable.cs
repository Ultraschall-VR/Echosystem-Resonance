using UnityEngine;

public class VRInteractable : MonoBehaviour
{
    public Material DefaultMaterial;

    void Start()
    {
        // Deactivate collision with ramps which are for player movement only
        var ramps = GameObject.FindObjectOfType<Ramps>().RampColliders;

        foreach (var ramp in ramps)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), ramp);
        }
    }
}
