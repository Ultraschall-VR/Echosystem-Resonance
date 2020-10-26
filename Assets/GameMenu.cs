using UnityEngine;
using UnityEngine.Rendering;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _menuPrefab;

    [HideInInspector] public bool ShowMenu;

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
        
        ShowMenu = true;
        _currentMenu = Instantiate(_menuPrefab, Vector3.zero, Quaternion.identity);
        Calibrate();

        if (PlayerSpawner.Instance.IsMenu)
        {
            ShowMenuOverlay();
        }
    }

    private void Calibrate()
    {
        _canvas = _currentMenu.GetComponent<RectTransform>();
        _canvas.rotation = _camera.rotation;
        _canvas.position = _camera.forward * 2;
        _canvas.position = new Vector3(_canvas.position.x, _canvas.sizeDelta.y /4, _canvas.position.z);
    }

    public void ShowMenuOverlay()
    {
        if (_currentMenu != null)
        {
            _postProcessingVolume.profile = _menuPostProcessing;
            Calibrate();
            _currentMenu.SetActive(true);
        }
    }

    public void HideMenuOverlay()
    {
        if (_currentMenu != null)
        {
            _postProcessingVolume.profile = _gamePostProcessing;
            _currentMenu.SetActive(false);
        }
    }

    void Update()
    {
        if (ShowMenu)
        {
            ShowMenuOverlay();
            PlayerSpawner.Instance.IsMenu = true;
        }
        else
        {
            HideMenuOverlay();
            PlayerSpawner.Instance.IsMenu = false;
        }
        
        if (_playerInput.BButtonPressed.stateUp)
        {
            ShowMenu = !ShowMenu;
        }
    }
}
