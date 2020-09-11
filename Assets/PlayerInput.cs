using UnityEngine;
using Valve.VR;

public class PlayerInput : MonoBehaviour
{
    public GameObject ControllerLeft;
    public GameObject ControllerRight;

    public bool RightHanded;
    public Transform PlayerHand;

    public SteamVR_Action_Boolean TouchpadPressed;
    public SteamVR_Action_Vector2 TouchpadPosition;
    public SteamVR_Action_Boolean AnyTriggerPressed;

    public SteamVR_Action_Boolean LeftTriggerPressed;
    public SteamVR_Action_Boolean RightTriggerPressed;

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
    }
}
