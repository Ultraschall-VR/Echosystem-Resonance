using Echosystem.Resonance.UI;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class ControllerManager : MonoBehaviour

    {
        public static ControllerManager Instance;

        public Uncovering Uncovering;
        public EchoBlaster EchoBlaster;
        public SceneLoader SceneLoader;
        public ObjectInteraction ObjectInteraction;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}