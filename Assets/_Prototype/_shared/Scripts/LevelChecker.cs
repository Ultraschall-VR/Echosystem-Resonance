using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class LevelChecker : MonoBehaviour
    {
        [SerializeField] private LevelLock[] _levelPads;
        void Start()
        {
            if (SceneSettings.Instance.ResetProgress)
            {
                PlayerPrefs.SetInt("levelUnlocked", 0);
            }

            int levelUnlocked = PlayerPrefs.GetInt("levelUnlocked");
            
            Debug.Log(levelUnlocked);

            for (int i = 0; i < levelUnlocked+1; i++)
            {
                _levelPads[i]._unlocked = true;
            }
        }
    }
}