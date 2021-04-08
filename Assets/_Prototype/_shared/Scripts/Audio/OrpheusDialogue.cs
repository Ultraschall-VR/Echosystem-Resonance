using UnityEngine;

public class OrpheusDialogue : MonoBehaviour
{
    [HideInInspector]public AudioSource OrpheusAudioSource; 
    [SerializeField] private AudioClip[] _audioClips;
    

    private int _counter;

    void Start()
    {
        OrpheusAudioSource = GetComponent<AudioSource>();
    }

    /*
    void ContinueContinue()
    {
        _audioSource.PlayOneShot(_audioClips[_counter]);
        _counter++;
    }
    */
    
    public void PlayOrpheusIndex(int index, float delay)
    {
        if(_audioClips.Length == 0)
            return;
        
        OrpheusAudioSource.clip = _audioClips[index];
        OrpheusAudioSource.PlayDelayed(delay);
    }
    public void PlayOrpheusIndexNoDelay(int index)
    {
        if(_audioClips.Length == 0)
            return;
        
        OrpheusAudioSource.clip = _audioClips[index];
        OrpheusAudioSource.Play();
    }
}
