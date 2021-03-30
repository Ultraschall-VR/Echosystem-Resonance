using Echosystem.Resonance.Game;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class FocusedObject : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit;

        if(Observer.Player == null)
            return;

        Vector3 origin;
        Vector3 forward;

        if (SceneSettings.Instance.VREnabled)
        {
            origin = Observer.Player.GetComponent<PlayerInput>().ControllerRight.transform.position;
            forward = Observer.Player.GetComponent<PlayerInput>().ControllerRight.transform.forward;
        }
        else
        {
            origin = Observer.PlayerHead.transform.position;
            forward = Observer.PlayerHead.transform.forward;
        }

        if (Physics.Raycast(origin, forward, out hit, 10f))
        {
            Observer.FocusedGameObject = hit.collider.gameObject;
            Observer.FocusedPoint = hit.point;
        }
        
        else
        {
            Observer.FocusedGameObject = null;
            Observer.FocusedPoint = Vector3.zero;
        }
    }
}
