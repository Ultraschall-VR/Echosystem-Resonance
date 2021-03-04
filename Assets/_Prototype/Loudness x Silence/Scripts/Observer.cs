using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class Observer: MonoBehaviour
    {
        public static GameObject Player;
        public static GameObject PlayerHead;
        public static SilenceSphere CurrentSilenceSphere;
        public static SilenceSphere LastSilenceSphere;
        public static bool SilenceSphereExited;
        public static float LoudnessValue;
        public static GameObject FocusedGameObject;
        public static int CollectedObjects;
        public static int MaxCollectibleObjects;

        private void Update()
        {
            CheckForFirstSilenceSphereExit();
        }

        private void CheckForFirstSilenceSphereExit()
        {
            if (LoudnessValue > 0.0f)
            {
                SilenceSphereExited = true;
            }
        }
    }
}


