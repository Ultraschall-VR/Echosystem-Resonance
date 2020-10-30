using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameMenu;
    
    private void OnEnable()
    {
        if(GameStateMachine.Instance.CurrentGameState == GameStateMachine.Gamestate.MainMenu)
        {
            _mainMenu.SetActive(true);
            _gameMenu.SetActive(false);
        }
        else
        {
            _gameMenu.SetActive(true);
            _mainMenu.SetActive(false);
        }
            
    }
}