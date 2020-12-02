using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class EchoCollection : MonoBehaviour
    {
        [SerializeField] private GrabCaster _grabCaster;

        private void Update()
        {
            var leftHand = PlayerInput.Instance.ControllerLeft;

            RaycastHit hit;
            if (Physics.Raycast(leftHand.transform.position, leftHand.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.GetComponent<Echo>())
                {
                    _grabCaster.ShowCast(leftHand.transform.position, hit.collider.transform.position);
                }
                else
                {
                    _grabCaster.Hide();
                }
            }
        }
    }
}