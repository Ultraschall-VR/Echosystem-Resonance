using System;
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

        private bool _charging;
        private bool _charged;
        private float _charge = 0.0f;
        private float _chargeThreshold = 0.2f;
        private float _maxCharge = 2.0f;

        private float _ammo;
        private float _maxAmmo;
        private float _initialAmmo = 60;

        [SerializeField] private bool _debug;

        private void Start()
        {
            AmmoUp();
        }

        void Update()
        {
            if (_debug)
            {
                Debug();
            }
        }

        public void AmmoUp()
        {
            _ammo = _initialAmmo;
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
            _ammo = _ammo - 10;
            StartCoroutine(MuzzleExplosion(15));
            Instantiate(_overdrivePrefab, _tip.transform.position, _tip.transform.rotation);
        }

        private void SpawnBlast()
        {
            _ammo = _ammo - 1;
            StartCoroutine(MuzzleExplosion(8));
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
                _muzzleExplosion.localScale = new Vector3(scale*value, scale*value, scale*value);
                
                yield return null;
            }
            
            timer = 0.005f;
            t = 0.0f;
            
            while (t < timer)
            {
                t += Time.deltaTime;

                float value = Mathf.Lerp(1, 0, t / timer);
                
                _muzzleLight.intensity = value * 10;
                _muzzleExplosion.localScale = new Vector3(scale*value, scale*value, scale*value);
                
                yield return null;
            }
        }
    }
}


