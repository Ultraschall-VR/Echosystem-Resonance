using UnityEngine;

public class CinematicComponentEnabler : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _vrEnabled;
    [SerializeField] private MonoBehaviour _nonVR;
    void Start()
    {
        if (SceneSettings.Instance.VREnabled)
        {
            _vrEnabled.enabled = true;
            _nonVR.enabled = false;
        }
        else
        {
            _vrEnabled.enabled = false;
            _nonVR.enabled = true;
        }
    }
}
