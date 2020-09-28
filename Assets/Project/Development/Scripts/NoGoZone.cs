using UnityEngine;

public class NoGoZone : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _spawner;
    [SerializeField] private Transform _spawnPos;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _spawner.MovePlayer(_spawnPos.position, _spawnPos.rotation);
        }
    }
}
