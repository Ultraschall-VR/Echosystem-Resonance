using UnityEngine;

public class PauseMenuDisabler : MonoBehaviour
{
    void Update()
    {
        if (PlayStateMachine.CurrentPlayState == PlayStateMachine.PlayState.Pause)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
