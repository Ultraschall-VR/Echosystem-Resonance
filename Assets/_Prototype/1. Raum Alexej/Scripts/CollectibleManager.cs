using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Echosystem.Resonance.Prototyping
{
    public class CollectibleManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _completionSound;
        [SerializeField] private float _pause = 1;
        [SerializeField] private List<GameObject> _collectableMelodies;
        private AudioSource _audioSource;
        public static int Index = 0;
        public static int ListCount;
        private bool _allCollected = false;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();
            ListCount = _collectableMelodies.Count;

            _allCollected = false;

            foreach (var i in _collectableMelodies)
            {
                i.SetActive(false);
            }
        }

        private void Update()
        {
            if (Index < ListCount)
            {
                if (!_collectableMelodies[Index].GetComponent<AudioSource>().isPlaying)
                {
                    Play();
                }
            }

            if (Index == ListCount && !_allCollected)
            {
                _allCollected = true;
                StartCoroutine(PlayCompletionSound(_pause));
            }
        }

        private void Play()
        {
            _collectableMelodies[Index].SetActive(true);
            _collectableMelodies[Index].GetComponent<AudioSource>().FadeIn(1, 1);
            _collectableMelodies[Index].GetComponent<AudioSource>().Play();
        }

        private IEnumerator PlayCompletionSound(float duration)
        {
            yield return new WaitForSeconds(duration);
            _audioSource.PlayOneShot(_completionSound);
            yield return null;
        }
    }
}