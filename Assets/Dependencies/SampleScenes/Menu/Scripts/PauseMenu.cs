using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _menu;
    [SerializeField] private List<AudioSource> _menuSounds;

    public bool Active;
    
    private void Start()
    {
        _menu.enabled = false;

        EchosystemSceneManager.PauseMenu = this;
    }

    private void LateUpdate()
    {
        Active = _menu.enabled;
        
        if (Observer.Player != null)
        {
            var allAudioSources = FindObjectsOfType<AudioSource>().ToList();
            
            List<AudioSource> sceneAudioSources = new List<AudioSource>();

            foreach (var source in allAudioSources)
            {
                if (!_menuSounds.Contains(source))
                {
                    sceneAudioSources.Add(source);
                }
            }

            if (_menu.enabled)
            {

                Time.timeScale = 0f;
                PlayStateMachine.CurrentPlayState = PlayStateMachine.PlayState.Pause;
                
                foreach (var source in sceneAudioSources)
                {
                    source.Pause();
                }
                
                Vector3 euler = new Vector3(_menu.transform.eulerAngles.x, _menu.transform.eulerAngles.y, 0);

                _menu.transform.eulerAngles = euler;
            }
            else
            {
                Time.timeScale = 1;
                PlayStateMachine.CurrentPlayState = PlayStateMachine.PlayState.Game;
                
                foreach (var source in sceneAudioSources)
                {
                    source.UnPause();
                }
                
                _menu.transform.position =
                    Observer.PlayerHead.transform.position + Observer.PlayerHead.transform.forward * 3;
                _menu.transform.rotation = Observer.PlayerHead.transform.rotation;
            }
        }
    }

    private void Update()
    {
        if (SceneSettings.Instance.VREnabled)
            VrInput();
        else
            NonVrInput();
    }

    public void Toggle()
    {
        _menu.enabled = !_menu.enabled;
    }

    private void NonVrInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    private void VrInput()
    {
        if (Observer.PlayerInput.MenuPressed.stateDown)
        {
            Toggle();
        }
    }
}