using UnityEngine;
using Valve.VR;

namespace Echosystem.Resonance.Game
{
    public class PlayerInput : MonoBehaviour
    {
        public GameObject ControllerLeft;
        public GameObject ControllerRight;

        public GameObject ControllerLeftGhost;
        public GameObject ControllerRightGhost;

        public SteamVR_Action_Boolean TouchpadPressed;
        public SteamVR_Action_Vector2 TouchpadPosition;

        public SteamVR_Action_Boolean LeftTriggerPressed;
        public SteamVR_Action_Boolean RightTriggerPressed;
        
        public SteamVR_Action_Boolean MenuPressed;

        public GameObject Head;
        public GameObject Player;
        
        public static PlayerInput Instance;

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