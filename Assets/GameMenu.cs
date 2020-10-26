using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private PlayerInput _playerInput;

    private bool _showMenu;
    
    void Start()
    {
        _showMenu = false;
    }

    void Update()
    {
        if (_showMenu)
        {
            _canvas.SetActive(true);
        }
        else
        {
            _canvas.SetActive(false);
        }
        
        if (_playerInput.BButtonPressed.stateUp)
        {
            _showMenu = !_showMenu;
        }
    }
}
