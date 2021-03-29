using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class LevelLock : MonoBehaviour
    {
        public bool _unlocked;

        private bool _changed;

        // Start is called before the first frame update
        void Start()
        {
            transform.GetChild(0).transform.Find("LockSymbol").gameObject.SetActive(true);
            transform.GetChild(0).transform.Find("LevelText").gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_unlocked && !_changed)
            {
                _changed = true;

                transform.GetChild(0).transform.Find("LockSymbol").gameObject.SetActive(false);
                transform.GetChild(0).transform.Find("LevelText").gameObject.SetActive(true);
            }
        }
    }
}