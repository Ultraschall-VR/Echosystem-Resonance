using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Echosystem.Resonance.Prototyping;


public class PauseMenuEchosystem : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] private GameObject _pauseCamera;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        Input();
    }

    private void Input()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                ResumeToGame();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ResumeToGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
       // Observer.Player.GetComponent<FirstPersonController>().enabled = true;
       Observer.Player.SetActive(true);
       _pauseCamera.SetActive(false);
        GameIsPaused = false;

    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
     //   Observer.Player.GetComponent<FirstPersonController>().enabled = false;
        Observer.Player.SetActive(false);
        _pauseCamera.SetActive(true);
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
