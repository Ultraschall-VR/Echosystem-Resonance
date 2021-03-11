using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    public float LoudnessIncreaseTime;
    public float LoudnessDecreaseTime;
    public float EchoDropLifetime;
    public float LightsourceRadius;

    public bool NonVr;

    public bool Beacons;
    public bool EchoDrops;
    public bool PitchShifter;

    public bool DebugMode;
    public bool GodMode;
    
    public bool PlayerCanDie;
    public float RespawnTime;

    public float SpawnDelay;

    
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
