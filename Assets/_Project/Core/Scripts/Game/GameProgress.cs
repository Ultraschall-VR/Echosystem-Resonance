using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class GameProgress : MonoBehaviour
    {
        public static GameProgress Instance;

        public bool LearnedTeleport;
        public bool LearnedGrab;
        public bool LearnedUncover;
        public bool LearnedEchoblaster;
        
        private bool _hasLearnedTeleport;
        private bool _hasLearnedGrab;
        private bool _hasLearnedUncover;
        private bool _hasLearnedEchoblaster;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            
            SetPower();
        }

        public void SetPower()
        {
            _hasLearnedTeleport = LearnedTeleport;
            _hasLearnedGrab = LearnedGrab;
            _hasLearnedUncover = LearnedUncover;
            _hasLearnedEchoblaster = LearnedEchoblaster;
        }

        private void Update()
        {
            if (GameStateMachine.Instance.MenuOpen)
            {
                LearnedTeleport = false;
                LearnedGrab = false;
                LearnedUncover = false;
                LearnedEchoblaster = false;
            }
            
            else
            {
                LearnedTeleport = _hasLearnedTeleport;
                LearnedGrab = _hasLearnedGrab;
                LearnedUncover = _hasLearnedUncover;
                LearnedEchoblaster = _hasLearnedEchoblaster;
            }
        }
        
        public enum GameProgressPower
        {
            Teleport,
            Grab,
            Uncover,
            Echoblaster,
            None
        }
    }
}