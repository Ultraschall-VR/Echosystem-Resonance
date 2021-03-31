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
                    
                    if (!SceneSettings.Instance.PlayerCanDie)
                        return;
                    
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
            yield return new WaitForSeconds(SceneSettings.Instance.RespawnTime/5);
            
            float t = 0.0f;
            float timer = SceneSettings.Instance.RespawnTime / 5;

            while (t < timer)
            {
                t += Time.deltaTime;
                _currentLoudness = Mathf.Lerp(1, 0, t / timer);
                yield return null;
            }

            if (!SceneSettings.Instance.VREnabled)
            {
                Observer.Player.GetComponent<CharacterController>().enabled = false;
            }
            
            Observer.Player.transform.position = Observer.LastSilenceSphere.transform.position + new Vector3(0, 0.5f,0);

            if (!SceneSettings.Instance.VREnabled)
            {
                Observer.Player.GetComponent<CharacterController>().enabled = true;
            }

            yield return null;
        }
    }
}