using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("levelUnlocked", 0);
    }
}
