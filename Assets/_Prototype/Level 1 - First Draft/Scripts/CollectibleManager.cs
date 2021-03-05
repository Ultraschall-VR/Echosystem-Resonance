using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Echosystem.Resonance.Prototyping
{
    public class CollectibleManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _completionSound;
        [SerializeField] private AudioClip _midAchievementSound;
        [SerializeField] private float _pause = 1;
        [SerializeField] private int _midAchievementAfter;
        [SerializeField] private List<GameObject> _collectableMelodies;
        private AudioSource _audioSource;
        public static int Index = 0;
        public static int ListCount;
        public static bool AllCollected;
        private bool _midGoal;

        private void Awake()
        {
            Index = 0;
            ListCount = 0;
            AllCollected = false;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();
            ListCount = _collectableMelodies.Count;

            Observer.MaxCollectibleObjects = ListCount;

            AllCollected = false;
            _midGoal = false;

            foreach (var i in _collectableMelodies)
            {
                i.SetActive(false);
            }
        }

        private void Update()
        {
            if (SceneSettings.Instance.GodMode)
            {
                if(Index < ListCount)
                    Index++;
            }
            
            if (Index < ListCount)
            {
                if (!_collectableMelodies[Index].GetComponent<AudioSource>().isPlaying)
                {
                    Play();
                }
            }

            Observer.CollectedObjects = Index;

            if (Index == ListCount && !AllCollected)
            {
                AllCollected = true;
                StartCoroutine(PlayCompletionSound(_pause));
            }

            if (Index == _midAchievementAfter && !_midGoal)
            {
                _midGoal = true;
                StartCoroutine(PlayMidAchievementSound(_pause));
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
        
        private IEnumerator PlayMidAchievementSound(float duration)
        {
            yield return new WaitForSeconds(duration);
            _audioSource.PlayOneShot(_midAchievementSound);
            yield return null;
        }
    }
}