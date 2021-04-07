using UnityEngine;
using UnityEngine.Rendering;

public class CinemaModePostProcessing : MonoBehaviour
{
    private Volume _postProcessing;

    [SerializeField] private VolumeProfile _cinemaVolumeProfile;
    [SerializeField] private VolumeProfile _defaultProfile;
    void Start()
    {
        _postProcessing = GetComponent<Volume>();

        if (SceneSettings.Instance.CinemaMode)
            _postProcessing.profile = _cinemaVolumeProfile;
        else
            _postProcessing.profile = _defaultProfile;
    }
}
