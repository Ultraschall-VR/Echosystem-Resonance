using UnityEngine;

public class PlayStateMachine : MonoBehaviour
{
    public static PlayState CurrentPlayState;

    public enum PlayState
    {
        Game,
        Pause
    }
}
