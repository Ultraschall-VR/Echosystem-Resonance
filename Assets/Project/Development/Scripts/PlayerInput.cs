using UnityEngine;
using Valve.VR;

public class PlayerInput : MonoBehaviour
{
    public GameObject ControllerLeft;
    public GameObject ControllerRight;

    public Collider ControllerLeftCollider;
    public Collider ControllerRightCollider;

    public bool RightHanded;
    public Transform PlayerHand;

    public SteamVR_Action_Boolean TouchpadPressed;
    public SteamVR_Action_Vector2 TouchpadPosition;

    public SteamVR_Action_Boolean LeftTriggerPressed;
    public SteamVR_Action_Boolean RightTriggerPressed;

    public bool LeftGripPressed;
    public SteamVR_Action_Single LeftGripForce;

    public SteamVR_Action_Boolean RightAPressed;

    public GameObject Head;
    public GameObject Player;

    void Update()
    {
        if (RightHanded)
        {
            PlayerHand = ControllerRight.transform;
        }
        else
        {
            PlayerHand = ControllerLeft.transform;
        }

        if (LeftGripForce.axis >= 1)
        {
            LeftGripPressed = true;
        }
        else
        {
            LeftGripPressed = false;
        }
    }
}
