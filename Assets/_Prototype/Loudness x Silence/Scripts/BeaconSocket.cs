using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class BeaconSocket : MonoBehaviour
    {
        public bool IsOccupied = false;

        private AudioSource _audioSource;
        [SerializeField] private AudioClip _placementSound;
        private bool _triggered = false;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (IsOccupied && !_triggered)
            {
                _triggered = true;

                Placement();
            }
        }

        private void Placement()
        {
            AudioSource.PlayClipAtPoint(_placementSound, transform.position);
            StartCoroutine(FadeOut(1f));
        }

        private IEnumerator FadeOut(float duration)
        {
            _audioSource.FadeOut(duration);
            yield return new WaitForSeconds(duration);

            yield return null;
        }
    }
}