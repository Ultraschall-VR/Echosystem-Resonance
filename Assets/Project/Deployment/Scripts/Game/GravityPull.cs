using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Valve.VR;

namespace Echosystem.Resonance.Game
{
    public class GravityPull : MonoBehaviour
    {
        [SerializeField] private bool _debug;
        [SerializeField] private ObjectHighlighter _objectHighlighter;
        [SerializeField] private AudioSource _loopAudioSource;

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
            if (!GameProgress.Instance.LearnedGrab)
                return;

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
                if (_objectHighlighter.ActiveObject == null)
                    return;

                _loopAudioSource.Stop();
                _objectHighlighter.ActiveObject.GetComponent<Rigidbody>().useGravity = true;
                _objectHighlighter.ActiveObject.GetComponent<Rigidbody>().velocity =
                    PlayerInput.Instance.ControllerLeft.GetComponent<Rigidbody>().velocity * 1.5f;
                _objectHighlighter.Locked = false;
                StopAllCoroutines();
            }
        }

        private IEnumerator MoveObject(Transform target, bool isEcho)
        {
            if (!_loopAudioSource.isPlaying)
                _loopAudioSource.Play();
            
            Transform activeObject = _objectHighlighter.ActiveObject.transform;
            Rigidbody rb = _objectHighlighter.ActiveObject.GetComponent<Rigidbody>();
            
            float distance = Vector3.Distance(activeObject.position, target.position);
            float timer = 1.0f * distance / 2 + rb.mass/100;
            float t = 0.0f;

            while (t <= timer)
            {

                if (rb == null)
                    break;
                
                if (rb.GetComponent<Audiocards>() != null)
                {
                    rb.GetComponent<Audiocards>().PlayAudioCard();
                    break;
                }
                
                t += Time.deltaTime;

                if (_objectHighlighter.ActiveObject == null)
                {
                    rb.useGravity = true;
                    _loopAudioSource.Stop();
                    break;
                }

                if (isEcho)
                {
                    if (rb.transform.localScale.x > 0.01f)
                    {
                        rb.transform.localScale = Vector3.Lerp(rb.transform.localScale, Vector3.zero, t / timer);
                    }
                    
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

            _loopAudioSource.Stop();

            yield return null;
        }
    }
}