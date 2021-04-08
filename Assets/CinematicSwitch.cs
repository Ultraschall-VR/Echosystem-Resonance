using UnityEngine;

public class CinematicSwitch : MonoBehaviour
{
    [SerializeField] private bool _isVR;

    private void Start()
    {
        if (SceneSettings.Instance.VREnabled)
        {
            if(_isVR)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
        else
        {
            if(_isVR)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }
    }
}
