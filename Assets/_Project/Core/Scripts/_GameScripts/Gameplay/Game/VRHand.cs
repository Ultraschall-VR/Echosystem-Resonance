using UnityEngine;
using Valve.VR;

namespace Echosystem.Resonance.Game
{
    public class VRHand : MonoBehaviour
    {
        private Rigidbody _rb;
        private bool _initialized = false;

        [SerializeField] private GameObject _inputHand;
        [SerializeField] private bool _isRightHand;
        
        public SteamVR_Action_Vibration hapticAction;

        private bool _collision = false;

        [SerializeField] private Animator _animator;

        [SerializeField] private Transform _ring;
        
        [SerializeField] private AudioSource _audioSource;

        public bool Idle;
        public bool Bow;

        private void Start()
        {
            Invoke("Initialize", 0.5f);
        }

        private void Initialize()
        {
            _rb = GetComponent<Rigidbody>();
            _initialized = true;
        }

        private void Update()
        {
            if (!_initialized)
            {
                return;
            }

            _audioSource.pitch = (_rb.velocity.magnitude / 4) + 0.33f;
            _ring.eulerAngles = new Vector3(_ring.eulerAngles.x, _ring.eulerAngles.y, 0);
            transform.rotation = _inputHand.transform.rotation;
            
            _animator.SetBool("Idle", Idle);
        }

        private void OnCollisionStay(Collision other)
        {
            if (!_initialized) 
                return;

            // Haptic Feedback on Collision
            
            //if (_isRightHand)
            //{
            //    hapticAction.Execute(0, 0.05f, 70,
            //        Vector3.Distance(transform.position, _inputHand.transform.position) / 4,
            //        SteamVR_Input_Sources.RightHand);
            //}
            //else
            //{
            //    hapticAction.Execute(0, 0.05f, 70,
            //        Vector3.Distance(transform.position, _inputHand.transform.position) / 4,
            //        SteamVR_Input_Sources.LeftHand);
            //}
        }
    }
}