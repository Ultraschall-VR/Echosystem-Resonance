using System;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public bool Uncovering = false;
        public bool GrabState = false;
        public bool TeleportState = false;
        public bool AudioBowState = false;

        public static PlayerStateMachine Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}