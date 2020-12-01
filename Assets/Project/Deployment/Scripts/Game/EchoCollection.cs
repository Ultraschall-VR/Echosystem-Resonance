using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class EchoCollection : MonoBehaviour
    {
        [SerializeField] private TeleportCaster teleportCaster;

        private void Update()
        {
            var leftHand = PlayerInput.Instance.ControllerLeft;

            RaycastHit hit;
            if (Physics.Raycast(leftHand.transform.position, leftHand.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.GetComponent<Echo>())
                {
                    teleportCaster.ShowGrab(leftHand.transform.position, hit.collider.transform.position, 4);
                }
                else
                {
                    teleportCaster.Hide();
                }
            }
        }
    }
}