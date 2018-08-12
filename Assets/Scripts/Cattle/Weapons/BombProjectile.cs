using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Cattle.Weapons
{
    public class BombProjectile : BaseProjectile
    {
        [Header("Bomb Projectile")]
        public GameObject explosionPrefab;

        public float lifeTime = 5.0f;

        void Start()
        {
            Invoke(nameof(Explode), lifeTime);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            Explode();
        }

        void Explode()
        {
            GameObject explosion = Instantiate(explosionPrefab.gameObject, transform.position, transform.rotation);
            explosion.transform.parent = null;
            
            Destroy(gameObject);
        }
    }
}