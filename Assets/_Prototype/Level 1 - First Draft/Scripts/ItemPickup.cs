using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    [RequireComponent(typeof(AudioSource))]
    public class ItemPickup : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField] private AudioClip _pickupSound;

        private bool _triggered = false;
        //  [HideInInspector]
        //  public bool IsCollected;
        //  [HideInInspector]
        //  public bool IsAvailable;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_triggered)
            {
                _triggered = true;
                if (CollectibleManager.Index < CollectibleManager.ListCount)
                {
                    CollectibleManager.Index++;
                  //  Debug.Log("Index is: " + CollectibleManager.Index);
                }

                Pickup();
            }
        }

        private void Pickup()
        {
            AudioSource.PlayClipAtPoint(_pickupSound, transform.position);
            StartCoroutine(FadeOut(1f));
        }

        private IEnumerator FadeOut(float duration)
        {
            _audioSource.FadeOut(duration);
            yield return new WaitForSeconds(duration);
            gameObject.SetActive(false);

            yield return null;
        }
    }
}