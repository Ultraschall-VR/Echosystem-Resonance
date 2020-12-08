using Echosystem.Resonance.Game;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class IntroSceneVideo : MonoBehaviour
    {
        [SerializeField] private GameObject _curvedUI;

        void Update()
        {
            if (GameStateMachine.Instance.CurrentGameState == GameStateMachine.Gamestate.Intro)
            {
                _curvedUI.SetActive(true);
            }
            else
            {
                _curvedUI.SetActive(false);
            }
        }
    }
}