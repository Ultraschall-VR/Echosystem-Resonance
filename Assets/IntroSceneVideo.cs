using Echosystem.Resonance.Game;
using UnityEngine;
using UnityEngine.Video;

public class IntroSceneVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateMachine.Instance.CurrentGameState == GameStateMachine.Gamestate.Intro)
        {
            _videoPlayer.enabled = true;
        }
        else
        {
            _videoPlayer.enabled = false;
        }
    }
}
