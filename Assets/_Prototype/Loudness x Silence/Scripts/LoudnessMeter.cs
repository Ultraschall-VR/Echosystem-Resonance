using UnityEngine;


namespace Echosystem.Resonance.Prototyping
{
    public class LoudnessMeter : MonoBehaviour
    {
        public float _currentLoudness = 0.0f;
        
        void Update()
        {
            if (Observer.CurrentSilenceSphere == null)
            {
                _currentLoudness += Time.deltaTime / SceneSettings.Instance.LoudnessIncreaseTime;
                
                if(_currentLoudness >= 1.0f)
                {
                    _currentLoudness = 1.0f;
                }
            }
            
            else
            {
                _currentLoudness -= Time.deltaTime / SceneSettings.Instance.LoudnessDecreaseTime;
                
                if (_currentLoudness <= 0.0f)
                {
                    _currentLoudness = 0.0f;
                }
            }
            
            Observer.LoudnessValue = _currentLoudness;
        }
    }
}