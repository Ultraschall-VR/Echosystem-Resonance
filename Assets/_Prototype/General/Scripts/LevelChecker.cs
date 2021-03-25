using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class LevelChecker : MonoBehaviour
    {
        // Liste der freischaltbaren Tafeln   
        [SerializeField] private GameObject[] _levelPads;

        void Start()
        {
            int levelUnlocked = PlayerPrefs.GetInt("levelUnlocked", 1);
            Debug.Log("Unlocked Levels:" + PlayerPrefs.GetInt("levelUnlocked"));

            for (int i = 0; i < _levelPads.Length; i++)
            {
                if (i + 1 > levelUnlocked)
                    _levelPads[i].transform.Find("LockSymbol").gameObject.SetActive(true);
                //       _levelPads[i].transform.Find("......").gameObject.SetActive(false);
            }
        }
    }
}