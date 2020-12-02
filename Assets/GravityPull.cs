using System;
using System.Collections;
using UnityEngine;

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
    }

    private void DebugMode()
    {
        if (_objectHighlighter.ActiveObject != null)
        {
            if (Input.GetMouseButton(0))
            {
                _objectHighlighter.Locked = true;
                StartCoroutine(MoveObject(transform.position));
            }
        }
    }

    private IEnumerator MoveObject(Vector3 targetPos)
    {
        Vector3 activeObjectPos = _objectHighlighter.ActiveObject.transform.position;
        Rigidbody rb = _objectHighlighter.ActiveObject.GetComponent<Rigidbody>();


        float distance = Vector3.Distance(activeObjectPos, targetPos);
        float timer = 1.0f / distance;
        float t = 0.0f;

        while (t <= timer)
        {
            t += Time.deltaTime;

            if (_objectHighlighter.ActiveObject == null)
            {
                break;
            }

            rb.MovePosition(Vector3.Lerp(activeObjectPos, targetPos, t / timer));
            yield return null;
        }

        yield return null;
    }
}