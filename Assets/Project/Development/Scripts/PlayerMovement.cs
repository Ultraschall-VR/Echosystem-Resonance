using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private Teleportation _teleportation;

    public bool TeleportEnabled;
    public float TeleportMovementSpeed;
    public bool JoystickMovement;
    public float JoystickMovementSpeed = 2.0f;

    private Rigidbody _rigidbody;
    private CapsuleCollider _playerCollider;
    private float _speed = 0.0f;

    private bool _isJumping;
    private bool _isTeleporting;

    private float _collisionMass;
    private float _massDivider = 5;

    private Vector3 _teleportTarget;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<CapsuleCollider>();
        _isJumping = false;
        _isTeleporting = false;
        _speed = JoystickMovementSpeed;
    }

    void FixedUpdate()
    {
        CalculateCollider();
        CalculatePhysics();
        FixRotation();
        
        if (JoystickMovement)
        {
            CalculateJoystickMovement(); 
        }

        if (TeleportEnabled)
        {
            CalculateTeleportation();
        }
    }

    private void CalculateCollider()
    {
        _playerCollider.center = new Vector3(_playerInput.Head.transform.localPosition.x, 1,
            _playerInput.Head.transform.localPosition.z);
    }

    private void CalculatePhysics()
    {
        if (JoystickMovementSpeed - _collisionMass / _massDivider <= 0)
        {
            _speed = 0;
        }
        else
        {
            _speed = JoystickMovementSpeed - _collisionMass / _massDivider;
        }
    }

    private void CalculateTeleportation()
    {
        RaycastHit hit;

        if (_playerInput.RightAPressed.state)
        {
            _isTeleporting = false;
            
            _playerStateMachine.TeleportState = true;
                
            if (Physics.Raycast(_playerInput.ControllerRight.transform.position,
                _playerInput.ControllerRight.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("TeleportArea"))
                {
                    _teleportation.RaycastTarget.position = hit.point;
                    _teleportation.Show(_playerInput.ControllerRight.transform.position,hit.point);

                    var offsetPos = _playerInput.Head.transform.position - transform.position;

                    _teleportTarget = hit.point - offsetPos;
                    _teleportTarget.y = 0.0f;
                }
                else
                {
                    _teleportation.Hide();
                    _teleportTarget = Vector3.zero;
                }
            }
        }
        else
        {
            _playerStateMachine.TeleportState = false;
            _teleportation.Hide();

            if (_teleportTarget != Vector3.zero && !_isTeleporting)
            {
                _isTeleporting = true;
                Debug.Log("TELEPORT");
                
                StartCoroutine(MoveToPosition(_rigidbody, _teleportTarget));
            }
        }
    }

    private void CalculateJoystickMovement()
    {
        if (_playerInput.TouchpadPressed.state)
        {
            var movePos = _rigidbody.position + _playerInput.Head.transform.forward *
                          (_playerInput.TouchpadPosition.axis.y * Time.deltaTime * _speed);

            _rigidbody.MovePosition(movePos);
        }
    }

    private void FixRotation()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            _collisionMass = other.gameObject.GetComponent<Rigidbody>().mass;
        }
        else
        {
            _collisionMass = 1;
        }
    }
    
    private IEnumerator MoveToPosition(Rigidbody rb, Vector3 target)
    {
        float t = 0;
        float timer = 0.5f;
        
        while (t <= timer)
        {
            t += Time.fixedDeltaTime * TeleportMovementSpeed;

            rb.MovePosition(Vector3.Lerp(rb.transform.position, target, t));
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, rb.rotation, t));
            yield return null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _collisionMass = 1;
    }
}