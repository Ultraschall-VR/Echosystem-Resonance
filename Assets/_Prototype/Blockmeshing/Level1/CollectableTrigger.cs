﻿using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class CollectableTrigger : MonoBehaviour
    {
        [SerializeField] private Animator _openDoor;

        void Update()
        {
            if (CollectibleManager._allCollected == true)
            {
                _openDoor.SetBool("AllCollected", true);
            }
        }
    }
}