using UnityEngine;

public class Observer : MonoBehaviour
{
    public static Observer Instance;

    public GameObject Player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    
}
