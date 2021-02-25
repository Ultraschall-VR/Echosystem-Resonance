using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    public float LoudnessIncreaseTime;
    public float LoudnessDecreaseTime;
    public float EchoDropLifetime;

    public bool NonVr;

    public bool Beacons;
    public bool EchoDrops;
    public bool PitchShifter;
    
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
}
