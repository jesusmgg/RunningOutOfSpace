using UnityEngine;

namespace Cattle.Weapons
{
    public class BombProjectile : BaseProjectile
    {
        [Header("Bomb Projectile")]
        public float splashRange;

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            Explode();
        }

        void Explode()
        {
            Debug.Log("EXPLOSION!");
            
            Destroy(gameObject);
        }
    }
}