using System.Collections.Generic;
using Echosystem.Resonance.Game;
using UnityEngine;

namespace Echosystem.Resonance.UI
{

    public class UIHUD : MonoBehaviour
    {
        [SerializeField] private GameObject _orpheusObjectives;
        [SerializeField] private List<HUDObjective> _orpheusHUDObjectives;
        
        [SerializeField] private GameObject _oceanFloorObjectives;
        [SerializeField] private List<HUDObjective> _oceanFloorHUDObjectives;
        
        [SerializeField] private GameObject _coreObjectives;
        [SerializeField] private List<HUDObjective> _coreHUDObjectives;
        
        public static UIHUD Instance;


        public void SetCount(ObjectiveManagement.Objective objective, int max, bool initializeMaxValue)
        {
            if (_orpheusObjectives.activeSelf)
            {
                foreach (var orpheusHudObjective in _orpheusHUDObjectives)
                {
                    if (objective == orpheusHudObjective.Objective)
                    {
                        orpheusHudObjective.SetCount(max, initializeMaxValue);
                    }
                }
            }
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            CheckState();
        }

        private void CheckState()
        {
            HideAll();
            
            switch (GameStateMachine.Instance.CurrentGameState)
            {
                case GameStateMachine.Gamestate.Orpheus:

                    _orpheusObjectives.SetActive(true);
                    break;

                case GameStateMachine.Gamestate.OceanFloor:

                    _oceanFloorObjectives.SetActive(true);
                    break;

                case GameStateMachine.Gamestate.Core:

                    _coreObjectives.SetActive(true);
                    break;
                
                default:
                    
                    HideAll();
                    break;
            }
        }

        private void HideAll()
        {
            //_coreObjectives.SetActive(false);
            //_oceanFloorObjectives.SetActive(false);
            _orpheusObjectives.SetActive(false);
        }

        void Update()
        {
            FixRotation();
        }
        
        private void FixRotation()
        {
            var rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            transform.eulerAngles = rot;
        }
    }
}


