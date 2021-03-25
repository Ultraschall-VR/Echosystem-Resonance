using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        int levelUnlocked = PlayerPrefs.GetInt("levelUnlocked", 1);
        Debug.Log("Unlocked Levels:" + PlayerPrefs.GetInt("levelUnlocked"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
