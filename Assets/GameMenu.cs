using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{

    [SerializeField] private GameObject _canvas;
    [SerializeField] private PlayerInput _playerInput;
    
    // Start is called before the first frame update
    void Start()
    {
        _canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
