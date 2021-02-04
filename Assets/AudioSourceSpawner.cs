using UnityEngine;

public class AudioSourceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _audioSourcePrefab;

    public int Amount;
    
    void Start()
    {
        for (int i = 0; i < Amount; i++)
        {
            var randPos = new Vector3(Random.Range(-50, 50), 0.5f, Random.Range(-50, 50));

            Instantiate(_audioSourcePrefab, randPos, Quaternion.identity);
        }
    }
}
