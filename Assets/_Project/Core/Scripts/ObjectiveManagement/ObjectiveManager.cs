using System;
using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Echosystem.Resonance.ObjectiveManagement
{
    public class ObjectiveManager : MonoBehaviour
    {
        public List<ObjectiveClass> Objectives;

        private List<ObjectiveItem> _allObjectiveItems;

        public int CurrentObjectiveIndex;

        public bool ShowAll;

        private UIHUD _uihud;
        private void Start()
        {
            CurrentObjectiveIndex = 0;
            CheckForObjectiveItems();
            UpdateObjectiveGoal();
            
            Invoke("HandleHUDVisibility", 2f);
        }

        private void HandleHUDVisibility()
        {
            _uihud = FindObjectOfType<UIHUD>();
            
            if (Objectives.Count == 0)
            {
                _uihud.Hide();
                return;
            } 
            
            _uihud.Show();
        }

        public void UpdateObjective(Objective objective)
        {
            foreach (var obj in Objectives)
            {
                if (obj.ObjectiveType == ObjectiveType.CollectObjects)
                {
                    if (obj.Objective == objective || !obj.IsDone)
                    {
                        obj.CounterValue++;
                        UpdateObjectiveGoal();
                        
                        if (obj.CounterValue == obj.ObjectiveItems.Count)
                        {
                            obj.IsDone = true;
                            CurrentObjectiveIndex++;
                        }
                    }
                }

                if (obj.ObjectiveType == ObjectiveType.ReachTrigger)
                {
                    //
                }

                if (obj.ObjectiveType == ObjectiveType.None)
                {
                    //
                } 
            }
        }

        private void UpdateObjectiveGoal()
        {
            foreach (var objective in Objectives)
            {
                switch (objective.ObjectiveType)
                {
                    case ObjectiveType.CollectObjects:
                        objective.Counter = objective.CounterValue + "/" + objective.ObjectiveItems.Count;

                        break;

                    case ObjectiveType.ReachTrigger:
                        objective.Counter = null;

                        break;
                }
            }
        }

        private void CheckForObjectiveItems()
        {
            _allObjectiveItems = FindObjectsOfType<ObjectiveItem>().ToList();
            
            foreach (var objective in Objectives)
            {
                foreach (var objectiveItem in _allObjectiveItems)
                {
                    if (objectiveItem.Objective == objective.Objective)
                    {
                        objective.ObjectiveItems.Add(objectiveItem);
                    }
                }
            }
        }
    }

    [Serializable]
    public class ObjectiveClass
    {
        public Objective Objective;
        public string Name;
        public string Description;
        [HideInInspector] public int CounterValue;
        [HideInInspector] public string Counter;
        public List<ObjectiveItem> ObjectiveItems;
        public ObjectiveType ObjectiveType;
        public bool IsDone;
    }

    public enum Objective
    {
        MelodyCards,
        EscapePod,
        EchoCollection,
        EchoBlaster,
        Uncover,
        AudioRocks,
        FollowTheMushrooms,
        FindTheCore
    }


    public enum ObjectiveType
    {
        CollectObjects,
        ReachTrigger,
        None
    }
}