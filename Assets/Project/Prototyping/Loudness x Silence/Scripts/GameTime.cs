using TMPro;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class GameTime : MonoBehaviour
    {
        public float _time;

        [SerializeField] private TextMeshProUGUI _text;
    
        void Update()
        {
            _time += Time.deltaTime;
        
            float minutes = Mathf.Floor(_time / 60);
            float seconds = Mathf.RoundToInt(_time % 60);

            string minuteText = minutes.ToString();
            string secondText = seconds.ToString();
        
            if (minutes < 10)
            {
                minuteText = "0" + minutes.ToString();
            }

            if (seconds < 10)
            {
                secondText = "0" + seconds.ToString();
            }

            if(_text == null)
                return;
        
            _text.text = "Game Time: " + minuteText + ":" + secondText;
        }
    } 
}


