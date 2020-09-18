using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Teleportation _teleportation;

    public float MaxSpeed = 2.0f;

    private Rigidbody _rigidbody;
    private CapsuleCollider _playerCollider;
    private float _speed = 0.0f;

    private bool _isJumping;
    private bool _isTeleporting;

    private float _collisionMass;
    private float _massDivider = 5;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<CapsuleCollider>();
        _isJumping = false;
        _isTeleporting = false;
        _speed = MaxSpeed;
    }

    void FixedUpdate()
    {
        CalculateCollider();
        FixRotation();
        CalculateTouchpadMovement();
        CalculateTeleportation();

        if (MaxSpeed - _collisionMass / _massDivider <= 0)
        {
            _speed = 0;
        }
        else
        {
            _speed = MaxSpeed - _collisionMass / _massDivider;
        }
    }

    private void CalculateCollider()
    {
        _playerCollider.center = new Vector3(_playerInput.Head.transform.localPosition.x, 1,
            _playerInput.Head.transform.localPosition.z);
    }

    private void CalculateTeleportation()
    {
        RaycastHit hit;

        if (Physics.Raycast(_playerInput.ControllerRight.transform.position,
            _playerInput.ControllerRight.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("TeleportArea"))
            {
                _teleportation.RaycastTarget.position = hit.point;
            }
        }
    }

    private void CalculateTouchpadMovement()
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

    private void OnTriggerExit(Collider other)
    {
        _collisionMass = 1;
    }
}