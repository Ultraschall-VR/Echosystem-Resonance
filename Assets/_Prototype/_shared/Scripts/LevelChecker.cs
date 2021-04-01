using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class LevelChecker : MonoBehaviour
    {
        [SerializeField] private LevelLock[] _levelPads;
        
        private int _levelUnlocked;
        void Start()
        {
           Invoke("DelayedStart", .5f);
        }

        private void DelayedStart()
        {
            if (SceneSettings.Instance.ResetProgress)
            {
                PlayerPrefs.SetInt("levelUnlocked", 0);
            }

            _levelUnlocked = PlayerPrefs.GetInt("levelUnlocked");

            if (_levelPads.Length == _levelUnlocked)
            {
                return;
            }
            
            FindObjectOfType<OrpheusDialogue>().PlayOrpheusIndex(_levelUnlocked, 0);

            var cliplength = FindObjectOfType<OrpheusDialogue>().OrpheusAudioSource.clip.length;
            
            Invoke("WaitForOrpheus", cliplength);
            
        }

        private void WaitForOrpheus()
        {
            for (int i = 0; i < _levelUnlocked + 1; i++)
            {
                _levelPads[i]._unlocked = true;
            }
        }
        
    }
}