using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class LoudnessMeter : MonoBehaviour
    {
        private float _currentLoudness = 0.0f;

        private bool _resetPlayer = false;

        void Update()
        {
            if (Observer.CurrentSilenceSphere == null)
            {
                _currentLoudness += Time.deltaTime / SceneSettings.Instance.LoudnessIncreaseTime;

                if (_currentLoudness >= 1.0f && Observer.CurrentSilenceSphere == null)
                {
                    _currentLoudness = 1.0f;
                    _resetPlayer = true;
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

            HandlePlayerHealth();
        }

        private void HandlePlayerHealth()
        {
            if(!SceneSettings.Instance.PlayerCanDie)
                return;
            
            if (_resetPlayer)
            {
                StartCoroutine(ResetPlayer());
                _resetPlayer = false;
            }
        }

        private IEnumerator ResetPlayer()
        {
            Observer.Player.GetComponent<CharacterController>().enabled = false;

            float time = SceneSettings.Instance.RespawnTime / 2;
            
            yield return new WaitForSeconds(time);
            
            _currentLoudness = 0.0f;

            Observer.Player.transform.position = Observer.LastSilenceSphere.transform.position;
            
            Observer.Player.GetComponent<CharacterController>().enabled = true;
        }
    }
}