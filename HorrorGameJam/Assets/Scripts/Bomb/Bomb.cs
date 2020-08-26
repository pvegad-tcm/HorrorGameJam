using UnityEngine;

namespace Bomb
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _explodeForce = 10;
        [SerializeField] private float _radius = 50;
        [SerializeField] private float _verticalForce = 2f;
        
        public void Start()
        {
            StartExplosion();
        }

        private void StartExplosion()
        {
            var explodePosition = transform.position;
            var colliders = Physics.OverlapSphere(explodePosition, _radius);
            foreach (var collision in colliders)
            {
                var rb = collision.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(_explodeForce, explodePosition, _radius, _verticalForce, ForceMode.Impulse);
                }
            }
        }
    }
}