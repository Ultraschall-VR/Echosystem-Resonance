using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class EchoBlaster : MonoBehaviour
    {
        [SerializeField] private Transform _tip;
        [SerializeField] private GameObject _blastPrefab;
        [SerializeField] private GameObject _overdrivePrefab;
        [SerializeField] private Transform _muzzleExplosion;
        [SerializeField] private Light _muzzleLight;
        
        private PlayerInput _playerInput;

        private bool _charging;
        private bool _charged;
        private float _charge = 0.0f;
        private float _chargeThreshold = 0.2f;
        private float _maxCharge = 2.0f;

        private float _ammo;
        private float _maxAmmo;
        private float _initialAmmo = 0;

        [SerializeField] private bool _debug;
        [SerializeField] private bool _isAI;

        public bool PlayerInRange;

        private bool _unlimitedAmmo;
        private bool _firing;

        private void Start()
        {
            _playerInput = PlayerInput.Instance;

            if (_isAI)
            {
                _unlimitedAmmo = true;
            }

            AmmoUp(_initialAmmo);
        }

        void Update()
        {
            if (_debug)
            {
                Debug();
            }
            else if (_isAI)
            {
                AIInput();
            }
            else
            {
                VRInput();
            }
        }

        public void AmmoUp(float amount)
        {
            if (_unlimitedAmmo)
            {
                _ammo = 1000000f;
            }
            else
            {
                _ammo = amount;
            }
        }

        private void AIInput()
        {
            if (PlayerInRange)
            {
                if (!_firing)
                {
                    StartCoroutine(AIFirePattern());
                    _firing = true;
                }
            }
        }

        private IEnumerator AIFirePattern()
        {
            SpawnBlast();

            yield return new WaitForSeconds(0.5f);

            SpawnBlast();

            yield return new WaitForSeconds(0.5f);

            SpawnBlast();

            yield return new WaitForSeconds(3f);

            yield return StartCoroutine(AIFirePattern());
        }

        private void VRInput()
        {
            if (_playerInput.RightTriggerPressed.stateUp)
            {
                if (!_charged)
                {
                    if (_ammo >= 1f)
                    {
                        SpawnBlast();
                    }
                }
                else
                {
                    if (_ammo >= 10f)
                    {
                        SpawnOverdrive();
                    }
                }

                _charging = false;
                _charged = false;
                _charge = 0.0f;
            }

            if (_playerInput.RightTriggerPressed.state)
            {
                _charge += Time.deltaTime;
            }

            if (_charge >= _chargeThreshold && !_charged)
            {
                _charging = true;
            }

            if (_charge >= _maxCharge)
            {
                _charging = false;
                _charged = true;
                _charge = _maxCharge;
            }
        }

        private void Debug()
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                if (!_charged)
                {
                    if (_ammo >= 1f)
                    {
                        SpawnBlast();
                    }
                }
                else
                {
                    if (_ammo >= 10f)
                    {
                        SpawnOverdrive();
                    }
                }

                _charging = false;
                _charged = false;
                _charge = 0.0f;
            }

            if (Input.GetKey(KeyCode.K))
            {
                _charge += Time.deltaTime;
            }

            if (_charge >= _chargeThreshold && !_charged)
            {
                _charging = true;
            }

            if (_charge >= _maxCharge)
            {
                _charging = false;
                _charged = true;
                _charge = _maxCharge;
            }
        }

        private void SpawnOverdrive()
        {
            _ammo -= 10;
            StartCoroutine(MuzzleExplosion(400));
            Instantiate(_overdrivePrefab, _tip.transform.position, _tip.transform.rotation);
        }

        private void SpawnBlast()
        {
            _ammo -= 1;
            StartCoroutine(MuzzleExplosion(250));
            Instantiate(_blastPrefab, _tip.transform.position, _tip.transform.rotation);
        }

        private IEnumerator MuzzleExplosion(float scale)
        {
            float timer = 0.05f;
            float t = 0.0f;

            while (t < timer)
            {
                t += Time.deltaTime;

                float value = Mathf.Lerp(0, 1, t / timer);

                _muzzleLight.intensity = value * 10;
                _muzzleExplosion.localScale = new Vector3(scale * value, scale * value, scale * value);

                yield return null;
            }

            timer = 0.1f;
            t = 0.0f;

            while (t < timer)
            {
                t += Time.deltaTime;

                float value = Mathf.Lerp(1, 0, t / timer);

                _muzzleLight.intensity = value * 10;
                _muzzleExplosion.localScale = new Vector3(scale * value, scale * value, scale * value);

                yield return null;
            }
        }
    }
}