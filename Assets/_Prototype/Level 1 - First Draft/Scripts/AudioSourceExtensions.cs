using System.Collections;

namespace UnityEngine
{
    public static class AudioSourceExtensions
    {
        // Fade Out -------------------------------
        public static void FadeOut(this AudioSource a, float duration)
        {
            a.GetComponent<MonoBehaviour>().StartCoroutine(FadeOutCore(a, duration));
        }

        private static IEnumerator FadeOutCore(AudioSource a, float duration)
        {
            float startVolume = a.volume;

            while (a.volume > 0)
            {
                a.volume -= startVolume * Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }

            a.Stop();
            a.volume = startVolume;
        }

        // Fade In -------------------------------
        public static void FadeIn(this AudioSource a, float duration, float max)
        {
            a.GetComponent<MonoBehaviour>().StartCoroutine(FadeInCore(a, duration, max));
        }

        public static IEnumerator FadeInCore(AudioSource a, float duration, float max)
        {
            float startVolume = 0.2f;
            a.volume = 0;
            while (a.volume < max)
            {
                a.volume += startVolume * Time.deltaTime / duration;
                yield return null;
            }

            a.volume = max;
        }

        ///////

        /// <summary>
        /// Helper class that destroys the gameobject as soon as
        /// <see cref="audioSource"/> is no longer playing. 
        /// Call <see cref="PlayClip2D(AudioClip)"/> to use this component.
        /// </summary>
        public class AutoCleanup : MonoBehaviour
        {
            /// <summary>
            /// Audio source to monitor. 
            /// </summary>
            public AudioSource audioSource;

            /// <summary>
            /// Tries to find the audio source automatically if not set.
            /// </summary>
            protected virtual void Start()
            {
                if (audioSource == null) audioSource = GetComponent<AudioSource>();

                if (audioSource == null)
                {
                    Debug.LogWarning(
                        "AutoCleanup has not audioSource and could not find any. This script has no effect, disabling.");
                    enabled = false;
                }
            }

            /// <summary>
            /// Destroys the game object when audioSource is no longer playing.
            /// </summary>
            protected virtual void Update()
            {
                if (!audioSource.isPlaying) Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Creates an audio player that removes itself after the given clip
        /// finished playing. Similar to <see cref="AudioSource.PlayClipAtPoint(AudioClip, Vector3)"/>, 
        /// but with preset for 2D sound and the ability to change the sound parameters
        /// through the return parameter.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <returns>The generated audio source which could be used to adjust audio source settings.</returns>
        public static AudioSource PlayClip2D(AudioClip clip, float volume)
        {
            GameObject go = new GameObject();
            AudioSource aus = go.AddComponent<AudioSource>();
            aus.spatialBlend = 0f;
            aus.clip = clip;
            aus.volume = volume;

            AutoCleanup r = go.AddComponent<AutoCleanup>();
            r.audioSource = aus;
            aus.Play();

            return aus;
        }
    }
}