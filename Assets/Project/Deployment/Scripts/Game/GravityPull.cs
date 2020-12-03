using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class GravityPull : MonoBehaviour
    {
        [SerializeField] private bool _debug;
        [SerializeField] private ObjectHighlighter _objectHighlighter;

        private void Update()
        {
            _objectHighlighter.Locked = false;

            if (_debug)
            {
                DebugMode();
            }

            VRInput();
        }

        private void DebugMode()
        {
            if (_objectHighlighter.ActiveObject != null)
            {
                if (Input.GetMouseButton(0))
                {
                    _objectHighlighter.Locked = true;
                    StartCoroutine(MoveObject(transform, false));
                }
            }
        }

        private void VRInput()
        {
            if (_objectHighlighter.ActiveObject != null)
            {
                if (PlayerInput.Instance.LeftTriggerPressed.state)
                {
                    _objectHighlighter.Locked = true;

                    if (_objectHighlighter.ActiveObject.GetComponent<Echo>())
                    {
                        StartCoroutine(MoveObject(PlayerInput.Instance.ControllerLeft.transform, true));
                    }
                    else
                    {
                        StartCoroutine(MoveObject(PlayerInput.Instance.ControllerLeft.transform, false)); 
                    }
                }
            }

            if (PlayerInput.Instance.LeftTriggerPressed.stateUp)
            {
                _objectHighlighter.ActiveObject.GetComponent<Rigidbody>().useGravity = true;
                _objectHighlighter.ActiveObject.GetComponent<Rigidbody>().velocity =
                    PlayerInput.Instance.ControllerLeft.GetComponent<Rigidbody>().velocity * 1.5f;
                _objectHighlighter.Locked = false;
                StopAllCoroutines();
            }
        }

        private IEnumerator MoveObject(Transform target, bool isEcho)
        {
            Transform activeObject = _objectHighlighter.ActiveObject.transform;
            Rigidbody rb = _objectHighlighter.ActiveObject.GetComponent<Rigidbody>();

            float distance = Vector3.Distance(activeObject.position, target.position);
            float timer = 1.0f * distance / 2;
            float t = 0.0f;

            while (t <= timer)
            {
                t += Time.deltaTime;
                
                if (_objectHighlighter.ActiveObject == null)
                {
                    rb.useGravity = true;
                    break;
                }

                if (isEcho)
                {
                    rb.transform.localScale = Vector3.Lerp(rb.transform.localScale, Vector3.zero, t / timer);
                    
                    if (t >= 0.98f)
                    {
                        rb.GetComponent<Echo>().AddEnergy();
                        Destroy(rb.gameObject);
                    }
                }
                
                rb.useGravity = false;
                rb.MovePosition(Vector3.Lerp(activeObject.position, target.position + target.forward / 2, t / timer));
                rb.MoveRotation(Quaternion.Lerp(activeObject.rotation, target.rotation, t / timer));
                yield return null;
            }

            yield return null;
        }
    }
}