using System.Collections;
using UnityEngine;
using Assets.Scripts.Core; //Namespace for PlayerController!

namespace Assets.Scripts.Object
{
    public class BlinkObject : MonoBehaviour
    {
        #region Serialize Fields

        [Header("Blink Settings")]
        [Space(15)]
        [SerializeField] private float _offMode;
        [SerializeField] private float _onMode;

        [Header("Explosion Settings")]
        [Space(15)]
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionForce;

        #endregion

        #region private Fields
        private Renderer _objectRenderer;
        private Vector3 _explosionCenter;

        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            _objectRenderer = GetComponent<Renderer>();
            _explosionCenter = transform.position;
        }

        private void Start() => StartCoroutine(BlinkCoroutine());

        private void Update() => HandlerExplosion();

        #endregion

        #region Private Methods
        private IEnumerator BlinkCoroutine()
        {
            while (true)
            {

                if (_objectRenderer == null)
                    yield break;

                yield return new WaitForSeconds(_offMode);
                _objectRenderer.enabled = !_objectRenderer.enabled;

                yield return new WaitForSeconds(_onMode);
                _objectRenderer.enabled = !_objectRenderer.enabled;
            }
        }

        private void HandlerExplosion()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Explosion(_explosionCenter, _explosionRadius, _explosionForce);
        }

        private void Explosion(Vector3 explosionCenter, float explosionRadius, float explosionForce)
        {
            // Encontrar todos los objetos en el radio de explosión
            Collider[] objectsInRange = Physics.OverlapSphere(explosionCenter, explosionRadius);

            foreach (Collider obj in objectsInRange)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Aplicar fuerza de explosión
                    rb.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);

                    // Si es el player, activar ragdoll
                    PlayerController player = obj.GetComponent<PlayerController>();
                    if (player != null)
                    {
                        obj.GetComponent<Animator>().enabled = false; // Desactivar Animator
                    }
                }
            }
        }
        #endregion

    }
}