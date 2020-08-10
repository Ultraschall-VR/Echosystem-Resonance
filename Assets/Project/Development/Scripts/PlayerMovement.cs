using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeed = 2.0f;
    public float JumpingCooldown;

    [SerializeField] private SteamVR_Action_Boolean _jumpPress = null;
    [SerializeField] private SteamVR_Action_Vector2 _moveValue;


    [SerializeField] private Transform _head;

    private Rigidbody _rigidbody;
    private CapsuleCollider _playerCollider;
    private float _speed = 0.0f;

    private bool _isJumping;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<CapsuleCollider>();
        _isJumping = false;
    }

    void Update()
    {
        CalculateCollider();
        CalculateMovement();
        FixRotation();
    }

    private void CalculateCollider()
    {
        _playerCollider.center = new Vector3(_head.transform.localPosition.x, 1, _head.transform.localPosition.z);
    }

    private void CalculateMovement()
    {
        var movePos = _rigidbody.position + _head.transform.forward * (_moveValue.axis.y * Time.deltaTime * MaxSpeed);
        _rigidbody.MovePosition(movePos);

        if (_jumpPress.state && !_isJumping)
        {
            Debug.Log("Jump");
            
            _rigidbody.AddForce(Vector3.up*1000, ForceMode.Impulse);
            _isJumping = true;
            StartCoroutine(ResetJump(JumpingCooldown));
        }
    }

    private void FixRotation()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    IEnumerator ResetJump(float time)
    {
        yield return new WaitForSeconds(time);
        _isJumping = false;
    }
}