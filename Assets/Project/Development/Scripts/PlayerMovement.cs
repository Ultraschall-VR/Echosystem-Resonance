using System.Collections;
using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    public float MaxSpeed = 2.0f;

    private Rigidbody _rigidbody;
    private CapsuleCollider _playerCollider;
    private float _speed = 0.0f;

    private bool _isJumping;
    private bool _isTeleporting;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<CapsuleCollider>();
        _isJumping = false;
        _isTeleporting = false;
    }

    void Update()
    {
        CalculateCollider();
        FixRotation();
        CalculateTouchpadMovement();
    }

    private void CalculateCollider()
    {
        _playerCollider.center = new Vector3(_playerInput.Head.transform.localPosition.x, 1,
            _playerInput.Head.transform.localPosition.z);
    }

    private void CalculateTouchpadMovement()
    {
        if (_playerInput.TouchpadPressed.state)
        {
            var movePos = _rigidbody.position + _playerInput.Head.transform.forward *
                (_playerInput.TouchpadPosition.axis.y * Time.deltaTime * MaxSpeed);
            
            _rigidbody.MovePosition(movePos);
        }
    }

    private void FixRotation()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}