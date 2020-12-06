using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class TriggerAvalanche : MonoBehaviour
    {
        [SerializeField] private bool _triggered;
        [SerializeField] private GameObject[] _rocks;
        [SerializeField] AudioSource[] _audiosources;

        private bool audioStarted = false;

        private void Start()
        {
            _triggered = false;
        }

        private void Update()
        {
            if (_triggered == true)
            {
                SetRigidbodies();
            }
        }

        private void SetRigidbodies()
        {
            foreach (var rock in _rocks)
            {
                rock.SetActive(true);
                rock.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Whale"))
            {
                SetRigidbodies();
                foreach (AudioSource i in _audiosources)
                {
                    i.Play();
                }

                // Sets audioStarted = true, so Player can't trigger it again
                audioStarted = true;
            }
        }
    }
}