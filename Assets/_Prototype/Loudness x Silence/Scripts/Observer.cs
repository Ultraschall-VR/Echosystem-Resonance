using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class Observer: MonoBehaviour
    {
        public static GameObject Player;
        public static GameObject PlayerHead;
        public static SilenceSphere CurrentSilenceSphere;
        public static SilenceSphere LastSilenceSphere;
        public static bool IsRespawning;
        public static bool SilenceSphereExited;
        public static float LoudnessValue;
        public static GameObject FocusedGameObject;
        public static int CollectedObjects;
        public static int MaxCollectibleObjects;

        private void Awake()
        {
            Player = null;
            PlayerHead = null;
            CurrentSilenceSphere = null;
            LastSilenceSphere = null;
            IsRespawning = false;
            SilenceSphereExited = false;
            LoudnessValue = 0.0f;
            FocusedGameObject = null;
            CollectedObjects = 0;
            MaxCollectibleObjects = 0;
        }
    }
}


