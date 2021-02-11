using Echosystem.Resonance.UI;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class HideUI : MonoBehaviour
    {
        [SerializeField] private MenuController _menuController;

        private void OnEnable()
        {
            _menuController.ToggleMenu();
            GameProgress.Instance.LearnedEchoblaster = true;
            GameProgress.Instance.LearnedGrab = true;
            GameProgress.Instance.LearnedTeleport = true;
            GameProgress.Instance.LearnedUncover = true;
        }
    }
}