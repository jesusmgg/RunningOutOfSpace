using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Cattle.Weapons
{
    public class BombProjectile : BaseProjectile
    {
        [Header("Bomb Projectile")]
        public float splashRange;

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            Tilemap tilemap = other.gameObject.GetComponent<Tilemap>();
            
            Vector3 hitPosition = Vector3.zero;
            if (tilemap != null)
            {
                foreach (ContactPoint2D hit in other.contacts)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    StartCoroutine(DestroyTile(tilemap, hitPosition));
                }
            }
            
            Explode();
        }
        
        IEnumerator DestroyTile(Tilemap tilemap, Vector3 hitPosition)
        {
            yield return new WaitForFixedUpdate();
            tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            yield return null;
        }

        void Explode()
        {
            Debug.Log("EXPLOSION!");
            
            Destroy(gameObject);
        }
    }
}