using System.Collections.Generic;
using UnityEngine;


namespace Echosystem.Resonance.Prototyping
{
    public class CollectibleManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _completionSound; 
        [SerializeField] private List<GameObject> _collectableMelodies;
        public static int Index = 0;

        private void Start()
        {
            foreach (var i in _collectableMelodies)
            {
                i.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_collectableMelodies[Index].GetComponent<AudioSource>().isPlaying)
            {
                Play();
            }
        }

        private void Play()
        {
            _collectableMelodies[Index].SetActive(true);
            _collectableMelodies[Index].GetComponent<AudioSource>().FadeIn(1, 1);
            _collectableMelodies[Index].GetComponent<AudioSource>().Play();
        }
    }
}