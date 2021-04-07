using System;
using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _menu;
    [SerializeField] private List<AudioSource> _pauseMenuAudioSources;

    private void Start()
    {
        _menu.enabled = false;
    }

    private void LateUpdate()
    {
        if (Observer.Player != null)
        {
            var allAudioSources = FindObjectsOfType<AudioSource>().ToList();

            foreach (var source in _pauseMenuAudioSources)
            {
                if (allAudioSources.Contains(source))
                {
                    allAudioSources.Remove(source);
                }
            }

            if (_menu.enabled)
            {
                foreach (var source in allAudioSources)
                {
                    if (source.isPlaying)
                        source.Pause();
                }

                //Time.timeScale = 0;

                return;
            }

            foreach (var source in allAudioSources)
            {
                if (!source.isPlaying)
                    source.Play();
                
                //Time.timeScale = 1;
            }

            _menu.transform.position =
                Observer.PlayerHead.transform.position + Observer.PlayerHead.transform.forward * 3;
            _menu.transform.rotation = Observer.PlayerHead.transform.rotation;
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