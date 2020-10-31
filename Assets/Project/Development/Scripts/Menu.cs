using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private SubMenu _mainMenu;
    [SerializeField] private SubMenu _gameMenu;

    private bool _initialized = false;

    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (GameStateMachine.Instance.CurrentGameState == GameStateMachine.Gamestate.MainMenu)
        {
            _mainMenu.Show();
            _gameMenu.Hide();
        }
        else
        {
            _mainMenu.Hide();
            _gameMenu.Show();
        }
    }
}