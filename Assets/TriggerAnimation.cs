using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class TriggerAnimation : MonoBehaviour
    {
        [SerializeField] private bool triggered;
        Animator m_Animator;
        
        void Start()
        {
            m_Animator = gameObject.GetComponent<Animator>();
            triggered = false;
        }

        void Update()
        {
            if (triggered == true)
            {
                m_Animator.SetBool("triggered", true);
            }
        }
    }
}