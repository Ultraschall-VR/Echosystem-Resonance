using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class LevelChecker : MonoBehaviour
    {
        [SerializeField] private LevelLock[] _levelPads;

        void Start()
        {
            if (SceneSettings.Instance.ResetProgress == true)
            {
                PlayerPrefs.SetInt("levelUnlocked", 1);
            }

            if (PlayerPrefs.GetInt("levelUnlocked") == 0)
            {
                PlayerPrefs.SetInt("levelUnlocked", 1);
            }

            int levelUnlocked = PlayerPrefs.GetInt("levelUnlocked");
            Debug.Log("Unlocked Levels:" + PlayerPrefs.GetInt("levelUnlocked"));


            for (int i = 0; i < _levelPads.Length; i++)
            {
                if (i < levelUnlocked)
                {
                    _levelPads[i]._unlocked = true;
                }
            }
        }
    }
}