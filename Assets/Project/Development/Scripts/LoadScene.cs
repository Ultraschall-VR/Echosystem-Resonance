using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    
    private void OnEnable()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
