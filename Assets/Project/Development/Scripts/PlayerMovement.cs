using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeed = 2.0f;
    public float JumpingCooldown;

    [SerializeField] private SteamVR_Action_Boolean _touchpadPressed = null;
    [SerializeField] private SteamVR_Action_Vector2 _touchPadValue;


    [SerializeField] private Transform _head;

    private Rigidbody _rigidbody;
    private CapsuleCollider _playerCollider;
    private float _speed = 0.0f;

    private bool _isJumping;
    private bool _isTeleporting;

    [SerializeField] private GameObject _controllerRightTip;
    public bool TouchpadControll;
    [SerializeField] private float _teleportSpeed;

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

        if (TouchpadControll)
        {
            CalculateTouchpadMovement();
        }
        
        // TODO Add teleport
        else
        {
            //CalculateTeleportMovement();
        }
    }

    private void CalculateCollider()
    {
        _playerCollider.center = new Vector3(_head.transform.localPosition.x, 1, _head.transform.localPosition.z);
    }

    private void CalculateTouchpadMovement()
    {
        if (_touchpadPressed.state)
        {
            var movePos = _rigidbody.position + _head.transform.forward * (_touchPadValue.axis.y * Time.deltaTime * MaxSpeed);
            _rigidbody.MovePosition(movePos);
        }
    }

    // TODO Add teleport
    
    /*
    private void CalculateTeleportMovement()
    {
        RaycastHit hit;

        if (_touchpadPressed.state && !_isTeleporting)
        {
            _isTeleporting = true;
        }


        
        Vector3 direction = Quaternion.Euler(eulerAngles) * Vector3.forward;
        
        Vector2 end = player.position + ray.direction

        
        
        var index = 0;
        var lastPos = _controllerRightTip.transform.position;
        var distance = 1;

        for (int i = 0; i < 4; i++)
        {
            float angle = 90 / 4;
            Vector3 rotation = new Vector3(angle, 0f, 0f);
            
            Debug.DrawRay(lastPos, Vector3.forward, Color.black, distance);

            lastPos = _controllerRightTip.transform.position +
                      Quaternion.Euler(rotation) * distance;
        }
        //if(Physics.Raycast(_controllerRight.transform.position, _co)
    }
    */

    private void FixRotation()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    
    /*private IEnumerator MoveToPosition(GameObject target, Rigidbody interactable)
    {
        float t = 0;
        float timer = 0.5f;
        
        while (t <= timer)
        {
            t += Time.fixedDeltaTime / _teleportSpeed;
            interactable.MovePosition(Vector3.Lerp(target.transform.position, _tip.transform.position, t));
            interactable.MoveRotation(Quaternion.Lerp(target.transform.rotation, _tip.transform.rotation,t));
            yield return null;
        }
    }*/

    IEnumerator ResetJump(float time)
    {
        yield return new WaitForSeconds(time);
        _isJumping = false;
    }
}