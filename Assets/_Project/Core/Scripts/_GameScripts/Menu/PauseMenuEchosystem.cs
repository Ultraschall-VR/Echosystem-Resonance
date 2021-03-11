using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Echosystem.Resonance.Prototyping;
using TMPro;


public class PauseMenuEchosystem : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] private GameObject _pauseCamera;
    public GameObject pauseMenuUI;

    [SerializeField] private string _taskHeadlineContent;

    [TextArea(15,20)]
    [SerializeField] private string _taskTextContent;

    [SerializeField] private TextMeshProUGUI _taskHeadline;
    [SerializeField] private TextMeshProUGUI _taskText;
    void Update()
    {
        Input();
    }

    private void Start()
    {
        _taskHeadline.text = _taskHeadlineContent;
        _taskText.text = _taskTextContent;
    }

    private void Input()
    {
        if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
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
        //   Observer.Player.GetComponent<FirstPersonController>().enabled = true;
        Observer.Player.SetActive(true);
         _pauseCamera.SetActive(false);
        GameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        
        //   Observer.Player.GetComponent<FirstPersonController>().enabled = false;
            Observer.Player.SetActive(false);
           _pauseCamera.SetActive(true);
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}