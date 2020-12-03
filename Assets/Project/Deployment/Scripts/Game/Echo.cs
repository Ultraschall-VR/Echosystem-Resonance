using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class Echo : MonoBehaviour
    {
        [SerializeField] private int _energyAmount;

        public void AddEnergy()
        {
            ControllerManager.Instance.EchoBlaster.AmmoUp(_energyAmount);
        }
    }
}