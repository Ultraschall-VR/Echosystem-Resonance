using System.Threading;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private KeyCode _uncoveringKey;
    [SerializeField] private Uncovering _uncovering;

    public bool UncoveringPressed;

    private void Update()
    {
        if (Input.GetKey(_uncoveringKey))
        {
            UncoveringPressed = true;
        }
        else
        {
            UncoveringPressed = false;
        }
    }
}
