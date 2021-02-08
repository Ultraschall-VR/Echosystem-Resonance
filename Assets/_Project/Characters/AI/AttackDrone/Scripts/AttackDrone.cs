using System.Collections;
using Echosystem.Resonance.Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Echosystem.Resonance.AI
{
    public class AttackDrone : MonoBehaviour
    {
        [SerializeField] private Transform _eyeball;
        [SerializeField] private Transform _range;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private AttackDroneRange _attackDroneRange;
        [SerializeField] private EchoBlaster _echoBlaster;

        [SerializeField] private AudioSource _loopAudioSource;
        [SerializeField] private AudioSource _explosionAudioSource;

        private Transform _playerPos;

        public int Life = 5;

        private bool _destroyed;

        private void Update()
        {
            if (Life <= 0 && !_destroyed)
            {
                Destroy();
                _destroyed = true;
                return;
            }
            
            if(_destroyed)
                return;

            if (_attackDroneRange.PlayerInTrigger)
            {
                MoveToPlayer();
            }

            else
            {
                _rb.velocity = transform.forward * 1;
            }
        }

        private void MoveToPlayer()
        {
            StopAllCoroutines();
            _echoBlaster.PlayerInRange = true;
            _range.gameObject.SetActive(false);
            Vector3 direction = (_playerPos.position - transform.position).normalized;
            Quaternion lookDirection = Quaternion.LookRotation(direction);
            transform.rotation = lookDirection;

            if (transform.position.y <= _playerPos.position.y + 2)
            {
                transform.position = new Vector3(transform.position.x, _playerPos.position.y + 2, transform.position.z);
            }

            _eyeball.transform.localEulerAngles = new Vector3(0, 90, 0);

            if (Vector3.Distance(transform.position, _playerPos.position) <= 5f)
            {
                _rb.velocity = Vector3.zero;
            }
            else
            {
                _rb.velocity = transform.forward * 2;
            }
        }

        private void Destroy()
        {
            StopAllCoroutines();
            _loopAudioSource.Stop();
            _explosionAudioSource.PlayOneShot(_explosionAudioSource.clip);
            _rb.useGravity = true;
            _rb.constraints = RigidbodyConstraints.None;
            _range.gameObject.SetActive(false);
        }

        private void Start()
        {
            Invoke("Initialize", 2f);
        }

        private void Initialize()
        {
            transform.eulerAngles = new Vector3(0, Random.Range(0, 180), 0);
            _rb.useGravity = false;

            if (FindObjectOfType<PlayerMovement>())
            {
                _playerPos = FindObjectOfType<PlayerMovement>().transform;
            }

            StartCoroutine(EyeMovement());
            StartCoroutine(RandomDirection());
            StartCoroutine(RandomMovement());
        }

        private IEnumerator EyeMovement()
        {
            float timer = Random.Range(0.5f, 3);
            float t = 0.0f;
            float currentY = _eyeball.localEulerAngles.y;
            float currentZ = _eyeball.localEulerAngles.z;
            float randY = Random.Range(65, 115);
            float randZ = Random.Range(0, 20);

            yield return new WaitForSeconds(Random.Range(0, 2));

            while (t <= timer)
            {
                t += Time.deltaTime;

                float valueY = Mathf.Lerp(currentY, randY, t / timer);
                float valueZ = Mathf.Lerp(currentZ, randZ, t / timer);

                _eyeball.localEulerAngles = new Vector3(0, valueY, valueZ);

                yield return null;
            }

            yield return StartCoroutine(EyeMovement());
        }

        private IEnumerator RandomDirection()
        {
            float timer = Random.Range(1, 5);
            float t = 0.0f;
            Vector3 currentDirection = transform.eulerAngles;
            float randDir = Random.Range(0, 180);

            while (t <= timer)
            {
                t += Time.deltaTime;

                Vector3 value = Vector3.Lerp(currentDirection, new Vector3(0, randDir, 0), t / timer);

                transform.eulerAngles = value;

                yield return null;
            }

            yield return StartCoroutine(RandomDirection());
        }

        private IEnumerator RandomMovement()
        {
            float timer = Random.Range(2, 4);
            float t = 0.0f;
            float currentY = transform.position.y;
            float randY = Random.Range(2, 4);

            while (t <= timer)
            {
                t += Time.deltaTime;

                float value = Mathf.Lerp(currentY, randY, t / timer);

                transform.position = new Vector3(transform.position.x, value, transform.position.z);

                yield return null;
            }

            yield return StartCoroutine(RandomMovement());
        }
    }
}