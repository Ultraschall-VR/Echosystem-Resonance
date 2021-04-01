using UnityEngine;

public class OrpheusDialogue2D : MonoBehaviour
{
    public AudioSource OrpheusAudioSource2D; 
    [SerializeField] private AudioClip[] _audioClips;
    

    private int _counter;

    void Start()
    {
        OrpheusAudioSource2D = GetComponent<AudioSource>();
        
    }

  
    /*
    void ContinueContinue()
    {
        _audioSource.PlayOneShot(_audioClips[_counter]);
        _counter++;
    }
    */
    
    public void PlayOrpheus2DIndex(int index, float delay)
    {
        OrpheusAudioSource2D.clip = _audioClips[index];
     OrpheusAudioSource2D.PlayDelayed(delay);
    }
    public void PlayOrpheus2DIndexNoDelay(int index)
    {
        OrpheusAudioSource2D.clip = _audioClips[index];
        OrpheusAudioSource2D.PlayDelayed(0);
    }
}
