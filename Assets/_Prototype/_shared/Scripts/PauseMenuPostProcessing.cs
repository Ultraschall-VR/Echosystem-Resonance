using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenuPostProcessing : MonoBehaviour
{
    private Volume _postProcessing;

    [SerializeField] private VolumeProfile _pauseMenuProfile;
    [SerializeField] private VolumeProfile _defaultProfile;

    private void Start()
    {
        _postProcessing = GetComponent<Volume>();
    }

    void Update()
    {
        if (PlayStateMachine.CurrentPlayState == PlayStateMachine.PlayState.Pause)
            _postProcessing.profile = _pauseMenuProfile;
        else
            _postProcessing.profile = _defaultProfile;
    }
}
