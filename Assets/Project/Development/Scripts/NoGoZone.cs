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
        
        if(other.gameObject.GetComponent<VRInteractable>())
        {
            var interactable = other.gameObject.GetComponent<VRInteractable>();
            var offsetPos = new Vector3(interactable.InitialPos.x, interactable.InitialPos.y + 0.01f, interactable.InitialPos.z);
            
            
            other.transform.position = offsetPos;
            other.transform.rotation = interactable.InitialRot;
        }
    }
}
