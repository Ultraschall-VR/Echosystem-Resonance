using UnityEngine;

public class MenuController : MonoBehaviour
{
    private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;

    [SerializeField] private Menu _gameMenu;
    [SerializeField] private Menu _mainMenu;
    [SerializeField] private Menu _loadingMenu;

    private GameStateMachine.Gamestate _gamestate;
    private bool _toggleMenu;

    private bool _initialized = false;
    private bool _positionSet = false;

    private void Start()
    {
        _animator.SetBool("Show", false);
        _animator.SetBool("Hide", true);
        Invoke("Initialize", 1f);
    }

    private void Initialize()
    {
        _gamestate = GameStateMachine.Instance.CurrentGameState;
        _playerInput = FindObjectOfType<PlayerInput>();

        if (_gamestate == GameStateMachine.Gamestate.Orpheus || _gamestate == GameStateMachine.Gamestate.Loading)
        {
            _toggleMenu = true;
        }
        
        _animator.SetBool("Show", true);
        _animator.SetBool("Hide", false);
        
        _initialized = true;
    }

    private void Update()
    {
        if (!_initialized)
            return;
        
        HandleInput();
        HandleMenuVisibility(_toggleMenu);
    }

    public void ToggleMenu()
    {
        _toggleMenu = !_toggleMenu;
    }

    private void HandleInput()
    {
        if (_gamestate == GameStateMachine.Gamestate.Orpheus || _gamestate == GameStateMachine.Gamestate.Loading)
        {
            return;
        }
        
        if (_playerInput.BButtonPressed.stateDown)
        {
            ToggleMenu();
        }
    }

    private void HandleMenuVisibility(bool show)
    {
        if (_gamestate == GameStateMachine.Gamestate.Orpheus)
        {
            _gameMenu.Hide();
            _gameMenu.EnableColliders(false);
            _mainMenu.Show();
            _mainMenu.EnableColliders(true);
            _loadingMenu.Hide();
            _loadingMenu.EnableColliders(false);
        } 
        else if (_gamestate == GameStateMachine.Gamestate.Loading)
        {
            _gameMenu.Hide();
            _gameMenu.EnableColliders(false);
            _mainMenu.Hide();
            _mainMenu.EnableColliders(false);
            _loadingMenu.Show();
            _loadingMenu.EnableColliders(true);
        }
        else
        {
            _gameMenu.Show();
            _gameMenu.EnableColliders(true);
            _mainMenu.Hide();
            _mainMenu.EnableColliders(false);
            _loadingMenu.Hide();
            _loadingMenu.EnableColliders(false);
        }
        
        if (show)
        {
            GameStateMachine.Instance.MenuOpen = true;
            
            _animator.SetBool("Show", true);
            _animator.SetBool("Hide", false);
            
            SetMenuRotation();

            if (!_positionSet)
            {
                SetMenuPosition();
                _positionSet = true;
            }
        }
        else
        {
            GameStateMachine.Instance.MenuOpen = false;
            
            _gameMenu.EnableColliders(false);
            _mainMenu.EnableColliders(false);
            _loadingMenu.EnableColliders(false);
            _animator.SetBool("Show", false);
            _animator.SetBool("Hide", true);
            _positionSet = false;
        }
    }

    private void SetMenuRotation()
    {
        Vector3 rot = new Vector3(_playerInput.Head.transform.eulerAngles.x,_playerInput.Head.transform.eulerAngles.y, 0);

        transform.eulerAngles = rot;
    }
    
    private void SetMenuPosition()
    {
        Vector3 pos = transform.position;
        
        if (_gamestate != GameStateMachine.Gamestate.Orpheus)
        {
            if (_playerInput.Head.transform.position.y >= 1)
            {
                pos = new Vector3(_playerInput.Head.transform.position.x, _playerInput.Head.transform.position.y -1, _playerInput.Head.transform.position.z);
            }
            else
            {
                pos = new Vector3(_playerInput.Head.transform.position.x, 0, _playerInput.Head.transform.position.z);
            }
        }
        
        transform.position = pos + _playerInput.Head.transform.forward *1;
    }
}