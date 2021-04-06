using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Echosystem.Resonance.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Canvas _menuLevelMenu;
        [SerializeField] private Canvas _settingsLevelMenu;
        [SerializeField] private Canvas _level0Menu;
        [SerializeField] private Canvas _settingsLevel0;

        private void Start()
        {
            _menuLevelMenu.enabled = false;
            _settingsLevelMenu.enabled = false;
            _level0Menu.enabled = false;
            _settingsLevel0.enabled = false;
            
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                _menuLevelMenu.enabled = true;
            } 
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _level0Menu.enabled = true;
            }
        }
    }
}