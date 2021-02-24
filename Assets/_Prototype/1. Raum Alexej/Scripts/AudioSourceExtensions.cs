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
        
    }
}