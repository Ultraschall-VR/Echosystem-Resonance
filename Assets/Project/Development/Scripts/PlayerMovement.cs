using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private LineRendererCaster _lineRendererCaster;
    [SerializeField] private LayerMask _teleportIgnoreLayer;

    private bool _snapTurnReady = true;
    public bool TeleportEnabled;
    public float TeleportMovementSpeed;
    public float TeleportMaxRange;
    public bool JoystickMovement;
    public float JoystickMovementSpeed;

    private Rigidbody _rigidbody;
    private CapsuleCollider _playerCollider;
    private float _speed = 0.0f;

    private bool _isJumping;
    private bool _isTeleporting;

    private bool _heavyMassCollision;

    private float _collisionMass;
    private float _massDivider = 5;

    private Vector3 _teleportTarget;
    private bool _teleportCooldownDone;
    
    

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<CapsuleCollider>();
        _isJumping = false;
        _isTeleporting = false;
        _teleportCooldownDone = true;
        _speed = JoystickMovementSpeed;
        _rigidbody.detectCollisions = true;
    }
    
    private void Update()
    {
        if (PlayerSpawner.Instance.IsMenu)
        {
            return;
        }

        if (!GameProgress.Instance.LearnedTeleport)
        {
            return;
        }
        
        if (JoystickMovement)
        {
            CalculateJoystickMovement();
        }

        if (TeleportEnabled)
        {
            CalculateTeleportation();
        }

        if (PlayerSpawner.Instance.IsMenu)
        {
            return;
        }
        
        CalculateCollider();
        CalculatePhysics();
        FixRotation();
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
            _speed = 0.5f;
        }
        else
        {
            _speed = JoystickMovementSpeed - _collisionMass / _massDivider;
        }
    }

    private void CalculateTeleportation()
    {
        if (!_teleportCooldownDone)
        {
            return;
        }

        if (_playerStateMachine.GrabState || _playerStateMachine.Uncovering)
        {
            return;
        }

        if (_playerInput.RightAPressed.state)
        {
            _isTeleporting = false;

            _playerStateMachine.TeleportState = true;

            RaycastHit hit;

            if (Physics.Raycast(_playerInput.ControllerRight.transform.position,
                -_playerInput.ControllerRight.transform.up + _playerInput.ControllerRight.transform.forward, out hit,
                Mathf.Infinity, _teleportIgnoreLayer))
            {
                _lineRendererCaster.RaycastTarget.position = hit.point + hit.transform.up/8;
                
                if (hit.collider.CompareTag("TeleportArea"))
                {
                    if (Vector3.Distance(_playerInput.Player.transform.position, hit.point) <= TeleportMaxRange)
                    {
                        _lineRendererCaster.ShowValidTeleport(_playerInput.ControllerRight.transform.position, hit.point,
                            1);

                        var offsetPos = _playerInput.Head.transform.position - transform.position;

                        _teleportTarget = hit.point - offsetPos;
                        _teleportTarget.y = hit.point.y + 0.1f;
                    }
                    
                    else
                    {
                        _lineRendererCaster.ShowInvalidTeleport(_playerInput.ControllerRight.transform.position, hit.point,
                            1);
                        _teleportTarget = Vector3.zero;
                    }
                }
                else
                {
                    _lineRendererCaster.ShowInvalidTeleport(_playerInput.ControllerRight.transform.position, hit.point,
                        1);
                    _teleportTarget = Vector3.zero;
                }
            }
        }
        else
        {
            _playerStateMachine.TeleportState = false;
            _lineRendererCaster.Hide();

            if (_teleportTarget != Vector3.zero && !_isTeleporting)
            {
                var cooldown = Vector3.Distance(_playerInput.transform.position, _teleportTarget);

                _isTeleporting = true;
                _teleportCooldownDone = false;
                Invoke("CheckTeleportCooldown", cooldown / 10);


                StartCoroutine(MoveToPosition(_rigidbody, _teleportTarget));
            }
        }
    }

    private void CheckTeleportCooldown()
    {
        _teleportCooldownDone = true;
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

            if (_collisionMass > _rigidbody.mass && _rigidbody.velocity.magnitude > 1f)
            {
                _heavyMassCollision = true;
            }
            else
            {
                _heavyMassCollision = false;
            }
        }
        else
        {
            _collisionMass = 1;
            _heavyMassCollision = false;
        }
    }

    private IEnumerator MoveToPosition(Rigidbody rb, Vector3 target)
    {
        float t = 0;
        float timer = 0.5f;

        while (t <= timer)
        {
            if (_heavyMassCollision)
            {
                _rigidbody.detectCollisions = true;
                yield break;
            }
            
            _rigidbody.detectCollisions = false;

            t += Time.deltaTime * TeleportMovementSpeed;

            rb.MovePosition(Vector3.Lerp(rb.transform.position, target, t));
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, rb.rotation, t));
            yield return null;
        }
        
        _rigidbody.detectCollisions = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _collisionMass = 1;
        _heavyMassCollision = false;
    }
}