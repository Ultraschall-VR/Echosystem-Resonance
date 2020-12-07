using Echosystem.Resonance.Game;
using UnityEngine;
using UnityEngine.Video;

namespace Echosystem.Resonance.UI
{
    public class IntroSceneVideo : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;

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
}