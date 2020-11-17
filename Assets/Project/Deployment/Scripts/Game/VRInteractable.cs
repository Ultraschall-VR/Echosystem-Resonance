using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class VRInteractable : MonoBehaviour
    {
        [SerializeField] private Material _coveredMaterial;
        [SerializeField] private Material _uncoveredMaterial;
        [SerializeField] private MeshRenderer _mesh;

        [HideInInspector] public Vector3 InitialPos;
        [HideInInspector] public Quaternion InitialRot;

        public bool IsCovered;
        public bool isTeleportable;

        public Material ActiveMaterial;

        public float CollisionMass;


        void Start()
        {
            InitialPos = transform.position;
            InitialRot = transform.rotation;
            CollisionMass = 1;

            _mesh = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (IsCovered)
            {
                ActiveMaterial = _coveredMaterial;
            }
            else
            {
                ActiveMaterial = _uncoveredMaterial;
            }

            _mesh.material = ActiveMaterial;
        }

        public void OnCollisionStay(Collision other)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                CollisionMass = other.gameObject.GetComponent<Rigidbody>().mass;
            }
            else
            {
                CollisionMass = 1;
            }
        }

        public void OnCollisionExit(Collision other)
        {
            CollisionMass = 1;
        }
    }
}