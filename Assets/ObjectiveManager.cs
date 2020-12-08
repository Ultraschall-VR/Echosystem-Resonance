using Echosystem.Resonance.UI;
using UnityEngine;

namespace Echosystem.Resonance.ObjectiveManagement
{
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance;

        private UIHUD _uihud;

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

    public enum Objective
    {
        MelodyCards,
        EscapePod
    }
}

