using System;
using UnityEngine;
using UnityEngine.Rendering;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _menuPrefab;
    public bool ShowMenu;

    [SerializeField] private VolumeProfile _menuPostProcessing;
    [SerializeField] private VolumeProfile _gamePostProcessing;

    private Volume _postProcessingVolume;

    private GameObject _currentMenu;

    [SerializeField] private Transform _camera;
    private RectTransform _canvas;

    void Start()
    {
        _postProcessingVolume = FindObjectOfType<Volume>();
        _postProcessingVolume.profile = _gamePostProcessing;

        Vector3 pos = new Vector3(0, 2, 0);

        if (FindObjectOfType<Menu>())
        {
            _currentMenu = FindObjectOfType<Menu>().gameObject;
        }
        else
        {
            _currentMenu = Instantiate(_menuPrefab, pos, Quaternion.identity);
        }

        Calibrate();
    }

    private void Calibrate()
    {
        if (_currentMenu != null)
        {
            _canvas = _currentMenu.GetComponent<RectTransform>();

            Vector3 rot;

            if (GameStateMachine.Instance.CurrentGameState == GameStateMachine.Gamestate.MainMenu)
            {
                rot = new Vector3(_camera.eulerAngles.x, _camera.eulerAngles.y, 0);
            }
            else
            {
                rot = _camera.eulerAngles;
            }

            _canvas.eulerAngles = rot;

            if (GameStateMachine.Instance.CurrentGameState == GameStateMachine.Gamestate.MainMenu)
            {
                return;
            }

            _canvas.localPosition = _camera.position + _camera.forward * 2;
            _canvas.localPosition = new Vector3(_canvas.position.x, _camera.transform.position.y, _canvas.position.z);
        }
    }

    public void ShowMenuOverlay()
    {
        if (_currentMenu != null)
        {
            _currentMenu.GetComponent<CanvasGroup>().alpha = 1f;
            Calibrate();
        }
        else
        {
            _currentMenu = Instantiate(_menuPrefab, Vector3.zero, Quaternion.identity);
        }
        
        
    }

    public void HideMenuOverlay()
    {
        if (_currentMenu != null)
        {
            _currentMenu.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    void Update()
    {
        GameStateMachine.Instance.MenuOpen = ShowMenu;

        if (GameStateMachine.Instance.CurrentGameState == GameStateMachine.Gamestate.MainMenu)
        {
            ShowMenu = true;
            return;
        }

        if (ShowMenu)
        {
            ShowMenuOverlay();
        }
        else
        {
            HideMenuOverlay();
        }
        
        if (_playerInput.BButtonPressed.stateUp)
        {
            ShowMenu = !ShowMenu;
        }
    }
}