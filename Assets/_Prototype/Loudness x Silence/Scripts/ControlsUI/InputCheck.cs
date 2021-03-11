using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class InputCheck : MonoBehaviour
{
    private bool _keysPressed = false;
    private bool _playerRotated = false;
    private bool _animationTriggered = false;
    
    private Animator _anim;

    private Vector3 _lastPlayerRotation = Vector3.zero;
    private Vector3 _currentPlayerRotation = Vector3.zero;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        CheckKeyPress();
        CheckPlayerRot();
        PlayAnim();
    }

    private void PlayAnim()
    {
        if (!_animationTriggered && _playerRotated && _keysPressed)
        {
            _animationTriggered = true;
            _anim.Play("UI_task_Animation");
        }
    }

    private void CheckKeyPress()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.W))
        {
            _keysPressed = true;
        }
    }

    private void CheckPlayerRot()
    {
        _currentPlayerRotation = Observer.Player.transform.eulerAngles;

        if (_lastPlayerRotation != Vector3.zero && _lastPlayerRotation != _currentPlayerRotation)
        {
            _playerRotated = true;
        }

        _lastPlayerRotation = _currentPlayerRotation;
    }
}
