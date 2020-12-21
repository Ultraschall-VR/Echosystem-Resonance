using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class PrototypeProgress : MonoBehaviour
    {
        public static PrototypeProgress Instance;
        public List<Objective> Objectives;

        public GameObject PlayerInstance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    [System.Serializable]
    public class Objective
    {
        public Collider Trigger;
        public List<bool> Conditions;
    }
}