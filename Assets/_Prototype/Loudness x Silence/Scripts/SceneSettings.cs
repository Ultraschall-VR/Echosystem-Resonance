using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    public float LoudnessIncreaseTime;
    public float LoudnessDecreaseTime;
    
    public static SceneSettings Instance;
    
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
