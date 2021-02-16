using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Echosystem.Resonance.Prototyping;
using UnityStandardAssets.Characters.FirstPerson;


public class PauseMenuEchosystem : MonoBehaviour
{
    public static bool GameIsPaused = false;


    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (GameIsPaused) {
                ResumeToGame();
            }
            else {
                Pause();
            }
        }
    }

    public void ResumeToGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Observer.Player.GetComponent<FirstPersonController>().enabled = true;
        GameIsPaused = false;

    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Observer.Player.GetComponent<FirstPersonController>().enabled = false;
        GameIsPaused = true;
    }
}
